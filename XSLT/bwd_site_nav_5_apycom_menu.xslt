<xsl:stylesheet version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output omit-xml-declaration="yes" indent="yes"/>
  <xsl:template match="navigation">
    <div>
      <xsl:attribute name="class">
        <xsl:value-of select='normalize-space(@cssclass)'/>
        <xsl:text> topNavBar</xsl:text>
      </xsl:attribute>
      <xsl:attribute name="id">
        <xsl:text>menu</xsl:text>
      </xsl:attribute>
      <ul>
      <xsl:attribute name="class">
        <xsl:text>menu</xsl:text>
      </xsl:attribute>
        <xsl:for-each select="group">
          <li>
            <a>
              <xsl:attribute name="href">
                /default.aspx?page=<xsl:value-of select="@pageid"/>
              </xsl:attribute>
              <xsl:attribute name="class">
                <xsl:text>parent</xsl:text>
              </xsl:attribute>
              <span><xsl:value-of select="@name"/></span>
            </a>
            <xsl:if test="item">
              <div><ul>
                <xsl:for-each select="item">
                  <li>
                    <a>
                      <xsl:attribute name="href">
                        /default.aspx?page=<xsl:value-of select="@pageid"/>
                      </xsl:attribute>
                      <xsl:attribute name="class">
                        <xsl:text>parent</xsl:text>
                      </xsl:attribute>
                      <span><xsl:value-of select="@name"/></span>
                    </a>
                    <xsl:if test="subitem">
                      <div><ul>
                        <xsl:for-each select="subitem">
                          <li>
                            <a>
                              <xsl:attribute name="href">
                                /default.aspx?page=<xsl:value-of select="@pageid"/>
                              </xsl:attribute>
                              <xsl:attribute name="class">
                                <xsl:text>parent</xsl:text>
                              </xsl:attribute>
                              <span><xsl:value-of select="@name"/></span>
                            </a>
                            <xsl:if test="subitem3">
                              <div><ul>
                                <xsl:for-each select="subitem3">
                                  <li>
                                    <a>
                                      <xsl:attribute name="href">
                                        /default.aspx?page=<xsl:value-of select="@pageid"/>
                                      </xsl:attribute>
                                      <xsl:attribute name="class">
                                        <xsl:text>parent</xsl:text>
                                      </xsl:attribute>
                                      <span><xsl:value-of select="@name"/></span>
                                    </a>
                                    <xsl:if test="subitem4">
                                      <div><ul>
                                        <xsl:for-each select="subitem4">
                                          <li>
                                            <a>
                                              <xsl:attribute name="href">
                                                /default.aspx?page=<xsl:value-of select="@pageid"/>
                                              </xsl:attribute>
                                              <span><xsl:value-of select="@name"/></span>
                                            </a>
                                          </li>
                                        </xsl:for-each>
                                      </ul></div>
                                    </xsl:if>
                                  </li>
                                </xsl:for-each>
                              </ul></div>
                            </xsl:if>
                          </li>
                        </xsl:for-each>
                      </ul></div>
                    </xsl:if>
                  </li>
                </xsl:for-each>
              </ul></div>
            </xsl:if>
          </li>
        </xsl:for-each>
      </ul>
    </div>
  </xsl:template>

</xsl:stylesheet>