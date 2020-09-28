<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartMerge.aspx.cs" Inherits="PianoFileMerge2.StartMerge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Piano Extract & Merge.</title>
    <style type="text/css">
        .pianotheme {
            font-family: Helvetica, sans-serif,Arial;
            font-size: 14pt;
            font-weight: 300;
            line-height: 36px;
            color: #0a0e12;
        }

        div.hrule {
            width: 100%;
            padding: 0px;
            margin: 15px 0px 15px 0px;
            border: 1px solid gainsboro;
            border-bottom: 1px;
            -webkit-box-shadow: 0 1px 6px rgba(0, 0, 0, 0.125);
            -moz-box-shadow: 0 1px 6px rgba(0, 0, 0, 0.125);
            box-shadow: 0 1px 6px rgba(0, 0, 0, 0.125);
        }

        div.rounded {
            border: 2px solid #398bbd;
            border: 2px solid silver;
            padding: 10px 40px;
            background-color: transparent;
            -webkit-border-radius: 6px;
            -moz-border-radius: 6px;
            border-radius: 6px;
            -moz-border-radius: 6px; /* Firefox 3.6 and earlier */
        }

            div.rounded.outer {
                margin: 20px 0px 0px 100px;
                width: 680px;
                height: 850px;
                float: left;
            }

        div.space {
            margin: 10px;
        }

        .twoCol {
            padding: 8px 5px 8px 5px;
            width: 680px;
        }

            .twoCol.floatL {
                float: left;
                width: 300px;
            }

            .twoCol.floatR {
                float: right;
                width: 300px;
            }
    </style>

</head>
<body class="pianotheme">
    <form id="form2" runat="server">
        <div style="width: 1680px;">
            <div class="rounded outer">
                <h1>ETL - CSV File Extract & Merge.</h1>
                <div class="hrule"></div>
                <div class="twoCol">
                    <div style="margin-left: -5px;">
                        <div class="floatL">
                            <b>1.</b> File A:&nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Font-Size="20px" />
                        </div>
                        <div class="floatR">
                            <b>2.</b> File B:&nbsp;&nbsp;<asp:FileUpload ID="FileUpload2" runat="server" Font-Size="20px" />
                        </div>
                    </div>
                </div>
                <div class="hrule"></div>
                <b>3.</b>&nbsp;&nbsp;<asp:Button runat="server" ID="BtnUpload" Text="Upload CSV."
                    OnClick="BtnUpload_Click" Width="200px" Height="40px" CssClass="pianotheme" />
                <asp:Label runat="server" ID="LblStatus" Text="Message: Select file." />
                <div class="hrule"></div>
                <div style="float: left;">&nbsp;&nbsp;File A.</div>
                <div style="float: right;">File B.&nbsp;&nbsp;</div>
                <div class="twoCol" style="width: 680px; height: 410px; overflow-y: scroll;">
                    <div class="floatL" style="float: left;">
                        <asp:GridView ID="GrvDataFileA" CssClass="pianotheme" runat="server"
                            AutoGenerateColumns="True" HeaderStyle-BackColor="#0489B1"
                            HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="250px">
                        </asp:GridView>
                    </div>
                    <div class="floatR" style="float: right;">
                        <asp:GridView ID="GrvDataFileB" CssClass="pianotheme" runat="server"
                            AutoGenerateColumns="True" HeaderStyle-BackColor="#0489B1"
                            HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="340px">
                        </asp:GridView>
                    </div>
                </div>
                <div class="hrule"></div>
                <div class="twoCol" style="width: 680px;">
                    <div class="floatL" style="float: left;">
                        <b>4.</b>&nbsp;&nbsp;<asp:Button runat="server" ID="BtnMerge" Text="Merge."
                            OnClick="BtnMerge_Click" Width="200px" Height="40px" CssClass="pianotheme" />
                    </div>
                    <div class="floatR" style="float: right;">
                        <asp:Button runat="server" ID="Button2" Text="Refresh."
                            OnClick="BtnRefresh_Click" Width="200px" Height="40px" CssClass="pianotheme" />
                    </div>
                </div>
            </div>
            <div class="rounded" style="margin-top: 20px; padding: 40px; float: left; margin-left: 30px; background-color: whitesmoke;">
                <div class="floatR" style="float: left;">
                    <div class="twoCol" style="width: 680px;">
                        <div class="floatL" style="float: left; width: 180px;">
                            <asp:Label runat="server" ID="LblApp" Text="Working Title." />
                        </div>
                        <div class="floatR" style="float: left;">
                            <asp:TextBox ID="TxtApp" runat="server" Text="Piano FileMerge." Height="30px"
                                Width="320px" Font-Size="18px" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="twoCol" style="width: 680px;">
                        <div class="floatL" style="float: left; width: 180px;">
                            <asp:Label runat="server" ID="LblAppPublisher" Text="Publisher." />
                        </div>
                        <div class="floatR" style="float: left;">
                            <asp:TextBox ID="TxtAppPublisher" runat="server" Text="https://sandbox.tinypass.com/api/v3/" Height="30px"
                                Width="320px" Font-Size="18px" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="hrule"></div>
                    <div>Merged Data.</div>
                    <div class="hrule"></div>
                    <div style="height: 482px; overflow-y: scroll;">
                        <asp:GridView ID="GrvDataMerge" CssClass="pianotheme" runat="server"
                            AutoGenerateColumns="True" HeaderStyle-BackColor="#61A6F8"
                            HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="650px" Height="400px">
                        </asp:GridView>
                    </div>
                    <div class="hrule"></div>
                    <br />
                    <div style="width: 650px;">
                        <div style="float: right">
                            <asp:Button runat="server" ID="BtnDownload" Text="Download CSV." OnClick="BtnDownload_Click"
                                Width="180px" Height="40px" CssClass="pianotheme" /><b>&nbsp;&nbsp;5.</b>
                        </div>
                    </div>
                </div>
            </div>



        </div>
        <div style="float: right; width: 650px; margin-top: 40px;">

            <style>
                .button {
                    background-color: gray; /* Green */
                    border: 1px;
                    color: white;
                    width: 180px;
                    padding: 5px;
                    text-align: center;
                    display: inline-block;
                    font-size: 16px;
                    cursor: pointer;
                    border-radius: 12px;
                }

                .link {
                    color: white;
                    font-family: 'Century Gothic':Verdana,Arial;
                    text-decoration: none;
                    font-size: 18px;
                }
            </style>

            <div class="button">
                <a class="link" href='https://sandbox.tinypass.com/id/?response_type=code&client_id=o1sRRZSLlw&stage=mobile&disablesignup=true&screen=login&displaymode=popup&width=400px' target="_blank">Piano ID Login</a>
            </div>

        </div>
    </form>
</body>
</html>
