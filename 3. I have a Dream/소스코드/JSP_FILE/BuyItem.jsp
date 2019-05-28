<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
    
<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	String userId = request.getParameter("userId");
	String item = request.getParameter("item");
	String num = request.getParameter("number");
	int price = 100;
	int number = Integer.parseInt(num);
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	if( item.equals("potion") || item.equals("shield") ){
		price = 100;
	}
	
	String getCoin = "select profile.coin from profile where profile.userId=?";
	PreparedStatement pstmt_coin = conn.prepareStatement(getCoin);
	pstmt_coin.setString(1, userId);
	ResultSet rs = pstmt_coin.executeQuery();
	
	String getCount = "select potion.pCount from potion where potion.userId=? and potion.pName=?";
	PreparedStatement pstmt_count = conn.prepareStatement(getCount);
	pstmt_count.setString(1, userId);
	pstmt_count.setString(2, item);
	ResultSet rs1 = pstmt_count.executeQuery();
	
	int i_userCoin = 0;
	int i_count = 0;
	int a = 0;
	while(rs.next()){
		i_userCoin = rs.getInt(1);
	}
	while(rs1.next()){
		i_count = rs1.getInt(1);
		a = i_count;
	}
	
	i_userCoin -= price * number;
	i_count = i_count + number;
	
	if (i_userCoin < 0 || i_count > 10)
		return;
	
	
	else{
		String updateCoin = "update profile set profile.coin=? where profile.userId=?";
		PreparedStatement pstmt_coinUpdate = conn.prepareStatement(updateCoin);
		pstmt_coinUpdate.setInt(1, i_userCoin);
		pstmt_coinUpdate.setString(2, userId);
		pstmt_coinUpdate.execute();
		
		String updatePotion = "update potion set potion.pCount=? where potion.userId=? and potion.pName=?";
		PreparedStatement pstmt_potionUpdate = conn.prepareStatement(updatePotion);
		pstmt_potionUpdate.setInt(1, i_count);
		pstmt_potionUpdate.setString(2, userId);
		pstmt_potionUpdate.setString(3, item);
		pstmt_potionUpdate.execute();
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