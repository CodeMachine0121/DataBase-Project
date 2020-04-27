<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
                        <h2>Access your control system</h2>
                        <p class="lead text-muted">All the data will be encrypted</p>
                        </div>
                            <div class="form-group">
                                <asp:TextBox type="text" ID="name" runat="server" class="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                          
                            <div class="form-group">
                                <asp:TextBox type="password" ID="password" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
                            </div>
                    
                       
                            <div class="d-flex flex-row align-items-center justify-content-between">
                                <asp:Button ID="Sign_up" class="btn btn-primary" runat="server" Text="Sign up" OnClick="Sign_up_Click" />
                                 <asp:Button ID="Sign_in" class="btn btn-primary" runat="server" Text="Sign in" OnClick="Sign_in_Click" />         
                            </div>

                                     

                        </div>
                    </div>
            </div>

  

    <!--navigation-->
    <section class="bh-white py-3">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 text-center">
                    <a href="Detail.html" class="heading-brand text-primary">Detail</a>
                </div>
            </div>
        </div>
    </section>

    <!--footer-->
    <footer>
        <div class="container">
            <div class="row">
                <div class="col-12 text-center">
                    <ul class="list-inline">
                        <li class="list-inline-item"><a href="About.html">About</a></li>
                        <li class="list-inline-item"><a href="Privacy.html">Privacy</a></li>
                        <li class="list-inline-item"><a href="Connection.html">Connection</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>

    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/feather-icons/4.7.3/feather.min.js"></script>
    <script src="js/scripts.js"></script>
    
    
     
    </form>
    </body>
</html>
