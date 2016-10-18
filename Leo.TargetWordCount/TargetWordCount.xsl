<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <html>
      <style>
        .Table {
        border-collapse: collapse,
        border: solid 1px #DDEEEE;
        }
        .Cell {
        text-align: left;
        padding: 8px;
        }
        .Header {
        background-color: #4CAF50;
        color: white;
        padding: 8px;
        }
      
      </style>
      <body>
        <xsl:for-each select="//File">
          <table class="Table">
            <caption>
              <xsl:value-of select="@Name" />
            </caption>
            <tr>
              <th class="Header">
                Type
              </th>
              <th class="Header">
                Segments
              </th>
              <th class="Header">
                <xsl:value-of select="@CountType" />
              </th>
              <th class="Header">
                Rate
              </th>
              <th class="Header">
                Amount
              </th>
            </tr>
            <tr>
              <td style="Cell">
                Context Match
              </td>
              <td style="Cell">
                <xsl:value-of select="//ContextMatch/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//ContextMatch/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//ContextMatch/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//ContextMatch/@Amount" />
              </td>
            </tr>
            <tr>
              <td>
                Repetitions
              </td>
              <td style="Cell">
                <xsl:value-of select="//Repetitions/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//Repetitions/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//Repetitions/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//Repetitions/@Amount" />
              </td>
            </tr>
            <tr>
              <td style="Cell">
                Cross-file Repetitions
              </td>
              <td style="Cell">
                <xsl:value-of select="//CrossFileRepetitions/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//CrossFileRepetitions/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//CrossFileRepetitions/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//CrossFileRepetitions/@Amount" />
              </td>
            </tr>
            <tr>
              <td style="Cell">
                100%
              </td>
              <td style="Cell">
                <xsl:value-of select="//OneHundredPercent/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//OneHundredPercent/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//OneHundredPercent/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//OneHundredPercent/@Amount" />
              </td>
            </tr>
            <tr>
              <td style="Cell">
                Context Match
              </td>
              <td style="Cell">
                <xsl:value-of select="//NinetyFivePercent/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//NinetyFivePercent/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//NinetyFivePercent/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//NinetyFivePercent/@Amount" />
              </td>
            </tr>
            <tr>
              <td style="Cell">
                85% - 94%
              </td>
              <td style="Cell">
                <xsl:value-of select="//EightyFivePercent/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//EightyFivePercent/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//EightyFivePercent/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//EightyFivePercent/@Amount" />
              </td>
            </tr>
            <tr>
              <td style="Cell">
                75% - 84%
              </td>
              <td style="Cell">
                <xsl:value-of select="//SeventyFivePercent/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//SeventyFivePercent/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//SeventyFivePercent/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//SeventyFivePercent/@Amount" />
              </td>
            </tr>
            <tr>
              <td style="Cell">
                50% - 74%
              </td>
              <td style="Cell">
                <xsl:value-of select="//FiftyPercent/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//FiftyPercent/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//FiftyPercent/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//FiftyPercent/@Amount" />
              </td>
            </tr>
            <tr>
              <td style="Cell">
                New
              </td>
              <td style="Cell">
                <xsl:value-of select="//New/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//New/@Count" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//New/@Rate" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//New/@Amount" />
              </td>
            </tr>
            <tr>
              <td>
                Total
              </td>
              <td style="Cell">
                <xsl:value-of select="//Total/@Segments" />
              </td>
              <td style="Cell">
                <xsl:value-of select="//Total/@Count" />
              </td>
              <td style="Cell">
              </td>
              <td style="Cell">
                <xsl:value-of select="//Total/@Amount" />
              </td>
            </tr>
          </table>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>