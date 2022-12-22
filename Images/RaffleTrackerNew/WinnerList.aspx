<%@ Page Title="Winner List 2021" Language="C#" AutoEventWireup="true" CodeFile="WinnerList.aspx.cs"
    Inherits="WinnerList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Raffle 2021- Winner List</title>
    <!-- html5 support in IE8 and later -->
    <!-- CSS file links -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/responsive.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/jquery.alerts.css" rel="stylesheet" />
 
    
</head>
<body>
    <form id="form1" runat="server">
        <header class="navbar navbar-default" style="height: 120px">
            <div class="topBar">
            </div>
            <div class="container">
                
              
                
                <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>

             <a class="navbar-brand" href="/login.aspx">
                        <img src="images/logo.jpg" alt="Raffle Ticket System" height="85" /></a>
                       
                         <span class="navbar-brand1" style="float:left;font-size:32px; font-weight:600; padding:30px 0 0 10px; font-family:Cursive;">Raffle Ticket Tracking</span>

            </div>
                           
                                
            </div>
            
    </header>
        <section class="properties" style="background-color: #e3e9ef!important">
           <div class="container" style="min-height: 540px; padding-top:0%">
  <div class="container">
    <div class="row">
      <div style="background-color: #428bca;">
        <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
      </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 400px">
      <div class="col-lg-12 col-md-12" style="padding-top: 15px">
        <div class="col-lg-12 col-sm-12">
          <div class="filterContent sidebarWidget register-form" style="border: none!important;">
            <div class="row">
              <div style="width: 100%">
                <h1>
                  Raffle-2021 Winner List
                </h1>
              </div>
              <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed">
                <thead style="background-color:#334b5f; color:#fff">
                  <tr>
                    <td>
                        PRIZE</td>
                   <td>
                       TICKET #
                   </td>
                    <td>
                      NAME ON TICKET
                    </td>
                    <td>
                      TOWN & STATE OF WINNER

                    </td>
                    <td>
                      PARISH TICKET SOLD FROM

                    </td>
                  </tr>
                </thead>
                <tbody>
                  <tr >
                    <td>
                        
                          1st Prize ($30,000)

                       
                    </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">71184</td>
                              </tr>
                          </table>

                    </td>
                    <td>
                       M. Cady

                    </td>
                  
                    <td style="font-size:12px">
                    Oxford Ma

                    </td>
                    <td>St. Roch Church Oxford
                    </td>
                  </tr>
            <%--      <tr>
                    <td>
                      2nd
                    </td>
                    <td>
                      $3,000
                    </td>
                    <td>
                      Audrey L Brogdon.
                    </td>
                    <td>
                      021950
                    </td>
                    <td>
                        Ms.Elaine Lesta
                    </td>
                   
                  </tr>
                  <tr>
                    <td>
                      3rd
                    </td>
                    <td>
                        $2,000
                    </td>
                    <td>
                      Ray Buxton
                    </td>
                    <td>
                      026198
                    </td>
                    <td>
                        Mr. and Mrs Ray Buxton
                    </td>
                   
                  </tr>
                  <tr>
                    <td>
                      4th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Gerardo Negron
                    </td>
                    <td>
                      106864
                    </td>
                    <td>
                        St. Peters
                    </td>
                    
                  </tr>
                  <tr>
                    <td>
                        5th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Kathy Biggs
                    </td>
                    <td>
                        027103
                    </td>
                    <td>
                        Mrs Marge McHugh
                    </td>
                   
                  </tr>
                  <tr>
                    <td>
                      6th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      John Maggi
                    </td>
                    <td>
                      054873
                    </td>
                    <td>
                        Mr. & Mrs. John Maggi
                    </td>
                   
                  </tr>
                  <tr>
                    <td>
                      7th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Joh and Patricia Grunert
                    </td>
                    <td>
                      098849
                    </td>
                    <td>
                        St. Mary Parish
                    </td>
                   
                  </tr>
                  <tr>
                    <td>
                      8th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Alison Chirip
                    </td>
                    <td>
                      100129
                    </td>
                     <td>
                        St. Joseph Parish
                    </td>
                   
                  </tr>
                  <tr>
                    <td>
                      9th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Michael and Marla Duffy
                    </td>
                    <td>
                      094631
                    </td>
                     <td>
                        St Mary's Church Bethel
                    </td>
                   
                  </tr>--%>

                 <%--    <tr>
                    <td>
                      10th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Tonya Montague
                    </td>
                    <td>
                      100470
                    </td>
                     <td>
                        St. Joseph Parish
                    </td>
                   
                  </tr>
                     <tr>
                    <td>
                      11th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Nancy Depalma
                    </td>
                    <td>
                      002322
                    </td>
                     <td>
                        Nancy Depalma
                    </td>
                   
                  </tr>
                     <tr>
                    <td>
                      12th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Aaycia Bischof
                    </td>
                    <td>
                      040362
                    </td>
                     <td>
                      Dr. and Mrs. Ralph Bischof
                    </td>
                   
                  </tr>
                     <tr>
                    <td>
                      13th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Anne Marie Keefe
                    </td>
                    <td>
                      055651
                    </td>
                     <td>
                        John Jachimowicz And Anne Marie Keefe
                    </td>
                   
                  </tr>
                     <tr>
                    <td>
                      14th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Barbara Casale
                    </td>
                    <td>
                      036207
                    </td>
                     <td>
                        Mr. and Mrs. Anthony Casale
                    </td>
                   
                  </tr>
                     
                     <tr>
                    <td>
                      15th
                    </td>
                    <td>
                        $1,000
                    </td>
                    <td>
                      Jill Deleo
                    </td>
                    <td>
                      025520
                    </td>
                     <td>
                      Dr. & Mrs. Arnold Isaacson
                    </td>
                   
                  </tr>--%>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">2nd Prize ($3,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">21950</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">A. Brogdon</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Greenwich Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Paul Parish Greenwich</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">3rd Prize ($2,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">26198</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">R. Buxton</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Stamford Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Clement Church</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">4th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">106864</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">G. Negron</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        &nbsp;</td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Peters</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">5th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">27103</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">K. Biggs</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Riverside Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Clement Church</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">6th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">54873</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">J. Maggi</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Highlands Nj</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Agnes Parish</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                     <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">7th Prize ($1,000)</td>
                              </tr>
                          </table>
                         </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">98849</td>
                              </tr>
                          </table>
                         </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">Grunert</td>
                            </tr>
                        </table>
                         </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Bethel Ct</td>
                            </tr>
                        </table>
                         </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St Mary&#39;s Church Bethel</td>
                            </tr>
                        </table>
                         </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">8th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">100129</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">A. Chirip</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Shelton Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Joseph Parish</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">9th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">94631</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">B. Duffy</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Bethel Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St Mary&#39;s Church Bethel</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">10th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">100470</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">T. Montague</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Shelton Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Joseph Parish</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">11th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">2322</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">N. DePalma</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Kensington Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Paul Parish Kensington</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">12th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">40362</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">A. Bischof</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Hainsport Nj</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">Our Lady Queen of Peace Church</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">13th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">55651</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">A. Keefe</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Atlantic Highlands Nj</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Agnes Parish</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">14th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">36207</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">B. Casale</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Delran Nj</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">Our Lady Queen of Peace Church</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                  <tr >
                    <td>
                        
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:92pt" width="122">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:4461;width:92pt" width="122" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td height="20" width="122">15th Prize ($1,000)</td>
                              </tr>
                          </table>
                      </td>
                      <td>
                          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:44pt" width="58">
                              <colgroup>
                                  <col style="mso-width-source:userset;mso-width-alt:2121;width:44pt" width="58" />
                              </colgroup>
                              <tr height="20" style="height:15.0pt">
                                  <td align="right" height="20" width="58">25220</td>
                              </tr>
                          </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:85pt" width="113">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:4132;width:85pt" width="113" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="113">J. Deleo</td>
                            </tr>
                        </table>
                      </td>
                  
                    <td style="font-size:12px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:134pt" width="179">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:6546;width:134pt" width="179" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="179">Stamford Ct</td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:160pt" width="213">
                            <colgroup>
                                <col style="mso-width-source:userset;mso-width-alt:7789;width:160pt" width="213" />
                            </colgroup>
                            <tr height="20" style="height:15.0pt">
                                <td height="20" width="213">St. Clement Church</td>
                            </tr>
                        </table>
                      </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
            <!-- end container -->
    </section>
        <div class="bottomBar">
            <div class="container">
                <p class="copyright">
                    &copy;2021 - Raffle Ticket Tracking
                </p>
            </div>
        </div>
    </form>
</body>
</html>
