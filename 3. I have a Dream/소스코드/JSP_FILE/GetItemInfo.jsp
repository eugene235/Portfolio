<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
    
<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	String userId = request.getParameter("userId");
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	String potion_sql = "select potion.pCount from potion where potion.userId=? and potion.pName='potion'";
	PreparedStatement potion_pstmt = conn.prepareStatement(potion_sql);
	potion_pstmt.setString(1, userId);
	ResultSet potion_rs = potion_pstmt.executeQuery();
	
	String shield_sql = "select potion.pCount from potion where potion.userId=? and potion.pName='shield'";
	PreparedStatement shield_pstmt = conn.prepareStatement(shield_sql);
	shield_pstmt.setString(1, userId);
	ResultSet shield_rs = shield_pstmt.executeQuery();
	
	String character_sql = "select characters.charName from characters where characters.userId=? and characters.charSelected=1";
	PreparedStatement character_pstmt = conn.prepareStatement(character_sql);
	character_pstmt.setString(1, userId);
	ResultSet char_rs = character_pstmt.executeQuery();
	
	String car_sql = "select car.carName from car where car.userId=? and car.carSelected=1";
	PreparedStatement car_pstmt = conn.prepareStatement(car_sql);
	car_pstmt.setString(1, userId);
	ResultSet car_rs = car_pstmt.executeQuery();
	
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body>

<%
	int potion_num = 0;
	int shield_num = 0;
	String char_name = null;
	String car_name = null;
	
	while(potion_rs.next()){
		potion_num = potion_rs.getInt(1);
	}

	while(shield_rs.next()){
		shield_num = shield_rs.getInt(1);
	}
	
	while(char_rs.next()){
		char_name = char_rs.getString(1);
	}
	
	while(car_rs.next()){
		car_name = car_rs.getString(1);
	}
%>

<h5><%= potion_num %></h5>
<h5><%= shield_num %></h5>
<h5><%= char_name %></h5>
<h5><%= car_name %></h5>

<%
	potion_rs.close();
	shield_rs.close();
	char_rs.close();
	car_rs.close();
	potion_pstmt.close();
	shield_pstmt.close();
	character_pstmt.close();
	car_pstmt.close();
	conn.close();
%>

</body>
</html>