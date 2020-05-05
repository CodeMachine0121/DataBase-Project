<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>


    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Crew - Free Bootstrap 4 Upcoming Landing Page Template</title>
    <meta name="description" content="Crew is a beautiful Bootstrap 4 template for upcoming landing pages."/>

    <!--Google font-->
    <link href="https://fonts.googleapis.com/css?family=K2D:300,400,500,700,800" rel="stylesheet">

    <!-- Bootstrap CSS / Color Scheme -->
    <link rel="stylesheet" href="css/bootstrap.css">

</head>
<body>
    <form id="form1" runat="server">
         <div class="col-md-5 pl-md-5">
                <div class="card">
                    <div class="card-body py-md-4">
                        <h2>註冊帳戶</h2>
                        <p class="lead text-muted">All the data will be encrypted</p>

                        </div>

                    <div>
                         <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
                    </div>

                    <div>
                        <asp:RadioButton ID="radio_STD" runat="server"  Text="學生" GroupName="identity" />
                         <asp:RadioButton ID="radio_PRO" runat="server"  Text="教授" GroupName="identity"/>
                        
                    </div>
                            <div class="form-group">
                                <asp:TextBox type="text" ID="id" runat="server" class="form-control" placeholder="Student ID"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox type="text" ID="name" runat="server" class="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox type="text" ID="email" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox type="password" ID="password" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
                            </div>
                            
                            <div class="form-group">
                                <asp:TextBox type="text" ID="cellphone" runat="server" class="form-control" placeholder="Cellphone"></asp:TextBox>
                            </div>
                                
                            <div class="d-flex flex-row align-items-center justify-content-between">
                                <a href="https://web.mcu.edu.tw/">Ming Chuan University</a>
                               
                                <asp:Button ID="Register" class="btn btn-primary" runat="server" Text="Sign up" OnClick="Register_Click" />
                            </div>
                        </div>
                    </div>
                </div>
    </form>
</body>
</html>
