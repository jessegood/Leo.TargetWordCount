using System.Diagnostics.Contracts;
namespace Leo.TargetWordCount
{
    using Models;
    using Sdl.Desktop.IntegrationApi;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    public partial class WordCountBatchTaskSettingsControl : UserControl, ISettingsAware<WordCountBatchTaskSettings>
    {
        private readonly Dictionary<RateType, string> displayString = new Dictionary<RateType, string>()
        {
            { RateType.PerfectMatch, "Perfect Match" },
            { RateType.ContextMatch, "Context Match" },
            { RateType.Repetitions, "Repetitions" },
            { RateType.CrossFileRepetitions, "Cross-file Repetitions" },
            { RateType.OneHundred, "100%" },
            { RateType.NinetyFive, "95% - 99%" },
            { RateType.EightyFive, "85% - 94%" },
            { RateType.SeventyFive, "75% - 84%" },
            { RateType.Fifty, "50% - 74%" },
            { RateType.New, "New" },
        };

        public WordCountBatchTaskSettingsControl()
        {
            InitializeComponent();
            Settings = new WordCountBatchTaskSettings();
        }

        public WordCountBatchTaskSettings Settings { get; set; }

        protected override void OnLeave(EventArgs e)
        {
            UpdateSettings();
        }

        protected override void OnLoad(EventArgs e)
        {
            Initialize();

            AddCultures();
            AddRows();

            dataGridView.CellEndEdit += DataGridView_CellEndEdit;
        }

        private void Initialize()
        {
            if (Settings.UseSource)
            {
                sourceRadioButton.Checked = true;
            }
            else
            {
                targetRadioButton.Checked = true;
            }

            reportLockedCheckBox.Checked = Settings.ReportLockedSeperately;
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var cell = dataGridView[e.ColumnIndex, e.RowIndex];
                decimal d;

                if (decimal.TryParse(cell.Value.ToString(), out d))
                {
                    cell.Value = d.ToString("C", CultureRepository.Cultures[cultureComboBox.SelectedItem.ToString()]);
                }
            }
        }

        private void AddCultures()
        {
            cultureComboBox.BeginUpdate();

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                cultureComboBox.Items.Add(culture.EnglishName);
            }

            cultureComboBox.Text = Settings.Culture;

            cultureComboBox.EndUpdate();
        }

        private void AddRows()
        {
            // Display all rows except RateType.Total
            for (int i = 0; i < Enum.GetValues(typeof(RateType)).Length - 1; ++i)
            {
                dataGridView.Rows.Add(new object[] { displayString[Settings.InvoiceRates[i].Type], Settings.InvoiceRates[i].Rate });
            }
        }

        private void UpdateSettings()
        {
            var invoiceRates = new List<InvoiceItem>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var rateType = displayString.First(p => p.Value == row.Cells[0].Value.ToString()).Key;
                var rate = row.Cells[1].Value.ToString();

                invoiceRates.Add(new InvoiceItem(rateType, rate));
            }

            Settings.InvoiceRates = invoiceRates;

            Settings.UseSource = sourceRadioButton.Checked;

            Settings.ReportLockedSeperately = reportLockedCheckBox.Checked;

            Settings.Culture = cultureComboBox.SelectedItem.ToString();
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(Settings != null);
        }
    }
}