<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
    
<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	String userId = request.getParameter("userId");
	String curChar = request.getParameter("currentCharacter");
	String curCar = request.getParameter("currentCar");
	
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	String up_char = "update characters set characters.charSelected=1 where characters.userId=? and characters.charName=?";
	PreparedStatement char_pstmt = conn.prepareStatement(up_char);
	char_pstmt.setString(1, userId);
	char_pstmt.setString(2, curChar);
	char_pstmt.execute();
	
	String up_char2 = "update characters set characters.charSelected=0 where characters.userId=? and characters.charName!=?";
	PreparedStatement char_pstmt2 = conn.prepareStatement(up_char2);
	char_pstmt2.setString(1, userId);
	char_pstmt2.setString(2, curChar);
	char_pstmt2.execute();
	
	
	String up_car = "update car set car.carSelected=1 where car.userId=? and car.carName=?";
	PreparedStatement car_pstmt = conn.prepareStatement(up_car);
	car_pstmt.setString(1, userId);
	car_pstmt.setString(2, curCar);
	car_pstmt.execute();
	
	String up_car2 = "update car set car.carSelected=0 where car.userId=? and car.carName!=?";
	PreparedStatement car_pstmt2 = conn.prepareStatement(up_car2);
	car_pstmt2.setString(1, userId);
	car_pstmt2.setString(2, curCar);
	car_pstmt2.execute();
	
	
%>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body>

</body>
</html>