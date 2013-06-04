<xsl:stylesheet version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output omit-xml-declaration="yes" indent="yes"/>
  <xsl:template match="navigation">
    <ul>
      <xsl:attribute name="class">
        <xsl:text>nav </xsl:text>
        <xsl:value-of select='normalize-space(@cssclass)'/>
      </xsl:attribute>
      <xsl:attribute name="role">
        <xsl:text>navigation</xsl:text>
      </xsl:attribute>
      <xsl:for-each select="group">
        <xsl:variable name="i" select="position()" />
        <li>
          <xsl:if test="item">
            <xsl:attribute name="class">
              <xsl:text>dropdown</xsl:text>
            </xsl:attribute>
          </xsl:if>
          <a>
            <xsl:if test="item">
              <xsl:attribute name="id">
                <xsl:value-of select="concat('drop', $i)"/>
              </xsl:attribute>
              <xsl:attribute name="data-toggle">
                <xsl:text>dropdown</xsl:text>
              </xsl:attribute>
              <xsl:attribute name="class">
                <xsl:text>dropdown-toggle</xsl:text>
              </xsl:attribute>
              <xsl:attribute name="data-target">
                <xsl:text>#</xsl:text>
              </xsl:attribute>
              <xsl:attribute name="role">
                <xsl:text>button</xsl:text>
              </xsl:attribute>
              <b class="caret"></b>
            </xsl:if>
            <xsl:attribute name="href">
              <xsl:text>/default.aspx?page=</xsl:text>
              <xsl:value-of select="@pageid"/>
            </xsl:attribute>
            <xsl:value-of select="@name"/>
          </a>
          <xsl:if test="item">
            <ul>
              <xsl:attribute name="class">
                <xsl:text>dropdown-menu </xsl:text>
              </xsl:attribute>
              <xsl:attribute name="role">
                <xsl:text>menu</xsl:text>
              </xsl:attribute>
              <xsl:attribute name="aria-labelledby">
                <xsl:value-of select="concat('drop', $i)"/>
              </xsl:attribute>
              <xsl:for-each select="item">
                <li>
                  <a>
                    <xsl:attribute name="href">
                      <xsl:text>/default.aspx?page=</xsl:text>
                      <xsl:value-of select="@pageid"/>
                    </xsl:attribute>
                    <xsl:value-of select="@name"/>
                  </a>
                  <!--
                  <xsl:if test="subitem">
                    <ul>
                      <xsl:attribute name="class">
                        <xsl:text>nav </xsl:text>
                      </xsl:attribute>
                      <xsl:for-each select="subitem">
                        <li>
                          <a>
                            <xsl:attribute name="href">
                              <xsl:text>/default.aspx?page=</xsl:text>
                              <xsl:value-of select="@pageid"/>
                            </xsl:attribute>
                            <xsl:value-of select="@name"/>
                          </a>
                          <xsl:if test="subitem3">
                            <ul>
                              <xsl:attribute name="class">
                                <xsl:text>nav </xsl:text>
                              </xsl:attribute>
                              <xsl:for-each select="subitem3">
                                <li>
                                  <a>
                                    <xsl:attribute name="href">
                                      <xsl:text>/default.aspx?page=</xsl:text>
                                      <xsl:value-of select="@pageid"/>
                                    </xsl:attribute>
                                    <xsl:value-of select="@name"/>
                                  </a>
                                  <xsl:if test="subitem4">
                                    <ul>
                                      <xsl:attribute name="class">
                                        <xsl:text>nav </xsl:text>
                                      </xsl:attribute>
                                      <xsl:for-each select="subitem4">
                                        <li>
                                          <a>
                                            <xsl:attribute name="href">
                                              <xsl:text>/default.aspx?page=</xsl:text>
                                              <xsl:value-of select="@pageid"/>
                                            </xsl:attribute>
                                            <xsl:value-of select="@name"/>
                                          </a>
                                        </li>
                                      </xsl:for-each>
                                    </ul>
                                  </xsl:if>
                                </li>
                              </xsl:for-each>
                            </ul>
                          </xsl:if>
                        </li>
                      </xsl:for-each>
                    </ul>
                  </xsl:if>
                  -->
                </li>
              </xsl:for-each>
            </ul>
          </xsl:if>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>

</xsl:stylesheet>