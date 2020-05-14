<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Professor.aspx.cs" Inherits="Professor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table BORDER="2" >
	<tr align=center>
		<td>節次/星期</td>
		<td>星期一</td>
		<td>星期二</td>
		<td>星期三</td>
		<td>星期四</td>
		<td>星期五</td>
	</tr>
	<tr align=center>
		<td>01</td>
		<td>
			<asp:Label ID="C0101" runat="server" ForeColor="Black" Text="X" ></asp:Label>
        </td>
		<td>
			<asp:Label ID="C0201" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0301" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0401" runat="server" ForeColor="Black" Text="X" ></asp:Label>
                          
		</td>
		<td>
			<asp:Label ID="C0501" runat="server" ForeColor="Black"  Text="X"></asp:Label>          
		</td>
	</tr>
	<tr align=center>
		<td>02</td>
		<td>
			<asp:Label ID="C0102" runat="server" ForeColor="Black" Text="X" ></asp:Label>
        </td>
		<td>
			<asp:Label ID="C0202" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0302" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0402" runat="server" ForeColor="Black" Text="X" ></asp:Label>
                          
		</td>
		<td>
			<asp:Label ID="C0502" runat="server" ForeColor="Black" Text="X" ></asp:Label>          
		</td>
	</tr>
	<tr align=center>
		<td>03</td>
		<td>
			<asp:Label ID="C0103" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0203" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0303" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0403" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0503" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
	</tr>
	<tr align=center>
		<td>04</td>
		<td>
			<asp:Label ID="C0104" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0204" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0304" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0404" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0504" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
	</tr>
	
	<tr align=center>
		<td>20</td>
		<td>午休</td>
		<td>午休</td>
		<td>午休</td>
		<td>午休</td>
		<td>午休</td>
	</tr>
	
	<tr align=center>
		<td>05</td>
		<td>
			<asp:Label ID="C0105" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0205" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0305" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0405" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0505" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>
	<tr align=center>
		<td>06</td>
		<td>
			<asp:Label ID="C0106" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0206" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0306" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0406" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0506" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr align=center>

	<tr align=center>	
		<td>07</td>
		<td>
			<asp:Label ID="C0107" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0207" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0307" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0407" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0507" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>
	
	<tr align=center>	
		<td>08</td>
		<td>
			<asp:Label ID="C0108" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0208" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0308" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0408" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0508" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>

	<tr align=center>	
		<td>09</td>
		<td>
			<asp:Label ID="C0109" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0209" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0309" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0409" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0509" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>
	<tr align=center>	
		<td>30</td>
		<td>
			<asp:Label ID="C0130" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0230" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0330" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0430" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0530" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>

	<tr align=center>	
		<td>40</td>
		<td>
			<asp:Label ID="C0140" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0240" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0340" runat="server" ForeColor="Black" Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0440" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0540" runat="server" ForeColor="Black"  Text="X" ></asp:Label>
		</td>
	</tr>

	<tr align=center>	
		<td>50</td>
		<td>
			<asp:Label ID="C0150" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0250" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0350" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0450" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0550" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>
	<tr align=center>	
		<td>60</td>
		<td>
			<asp:Label ID="C0160" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0260" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0360" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0460" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0560" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>
	<tr align=center>	
		<td>70</td>
		<td>
			<asp:Label ID="C0170" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0270" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0370" runat="server" ForeColor="Black" Text="X"></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0470" runat="server" ForeColor="Black"  Text="X" ></asp:Label>
		</td>
		<td>
			<asp:Label ID="C0570" runat="server" ForeColor="Black"  Text="X"></asp:Label>
		</td>
	</tr>
	</table>


        </div>
        
    </form>
</body>
</html>
