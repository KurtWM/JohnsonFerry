<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <style type="text/css">
      .element { color: #000000; }

      .name { color: Blue; }

      .value { color: Red; }

      .attribute { font-style: italic; }

    </style>
    <xsl:for-each select="*">
      <xsl:call-template name="testtemplate"></xsl:call-template>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="testtemplate">
    <xsl:copy>
      <xsl:apply-templates/>
    </xsl:copy>
  </xsl:template>


  <!-- Evaluate Elements -->
  <!--<xsl:template match="*" priority="10">-->
    <xsl:template name="pagenode">
      <div class="element">
      <!-- First, create the opening tag with the attributes -->
      &lt;<span class="name">
        <xsl:value-of select="name()"/>
        <xsl:text disable-output-escaping="yes"> </xsl:text>
      </span><xsl:apply-templates select="@*"/>&gt;
      <!-- Then, add children -->
      <xsl:apply-templates select="node()"/>
      <!-- Finally, add the closing tag -->
      &lt;/<span class="name">
        <xsl:value-of select="name()"/>
      </span>&gt;
    </div>
  </xsl:template>

  <!-- Evaluate Attributes -->
  <xsl:template match="@*">
    <span class="attribute">
      <span class="name">
        <xsl:value-of select="name()"/>
      </span>="<span class="value">
        <xsl:value-of select="." />
      </span>"
    </span>
  </xsl:template>

  <!-- Just copy everything else (text, comments, etc.) -->
  <xsl:template match="node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>