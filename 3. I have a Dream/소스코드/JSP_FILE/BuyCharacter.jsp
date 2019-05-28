<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>

<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	String userId = request.getParameter("userId");
	String item = request.getParameter("item");
	int price = 0;
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	if( item.equals("ch1") || item.equals("airballon")){
		price = 200;
	}
	else if (item.equals("ch2") || item.equals("rocket")){
		price = 400;
	}
	else if (item.equals("ch3") || item.equals("ufo")){
		price = 600;
	}
	
	
	String getCoin = "select profile.coin from profile where profile.userId=?";
	PreparedStatement pstmt_coin = conn.prepareStatement(getCoin);
	pstmt_coin.setString(1, userId);
	ResultSet rs = pstmt_coin.executeQuery();

	int i_userCoin = 0;
	int a = 0;
	
	while(rs.next()){
		i_userCoin = rs.getInt(1);
	}
	
	i_userCoin -= price;
	if (i_userCoin < 0)
		return;
	
	else{
		String updateCoin = "update profile set profile.coin=? where profile.userId=?";
		PreparedStatement pstmt_coinUpdate = conn.prepareStatement(updateCoin);
		pstmt_coinUpdate.setInt(1, i_userCoin);
		pstmt_coinUpdate.setString(2, userId);
		pstmt_coinUpdate.execute();
		
		String updateCharacters = "update characters set characters.charHave=true where characters.userId=? and characters.charName=?";
		PreparedStatement pstmt_charactersUpdate = conn.prepareStatement(updateCharacters);
		pstmt_charactersUpdate.setString(1, userId);
		pstmt_charactersUpdate.setString(2, item);
		pstmt_charactersUpdate.execute();
	}
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Insert title here</title>
</head>
<body>

</body>
</html>