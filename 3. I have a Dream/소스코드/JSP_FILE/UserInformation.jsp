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
	
	////////////////////////////// Get Data from table Profile //////////////////////////////
	String profile_sql = "select characters.charName, profile.coin, profile.score from profile, characters where profile.userId=? and characters.userId=? and characters.charSelected=1";
	PreparedStatement pstmt_profile = conn.prepareStatement(profile_sql);
	pstmt_profile.setString(1, userId);
	pstmt_profile.setString(2, userId);
	ResultSet rs = pstmt_profile.executeQuery();
	
	////////////////////////////// Get Data from table Potion //////////////////////////////
	String potion_sql = "select potion.pCount from potion where potion.userId=? and potion.pName='potion'";
	PreparedStatement pstmt_potion = conn.prepareStatement(potion_sql);
	pstmt_potion.setString(1, userId);
	ResultSet item_potion = pstmt_potion.executeQuery();
	
	String shield_sql = "select potion.pCount from potion where potion.userId=? and potion.pName='shield'";
	PreparedStatement pstmt_shield = conn.prepareStatement(shield_sql);
	pstmt_shield.setString(1, userId);
	ResultSet item_shield = pstmt_shield.executeQuery();

	////////////////////////////// Get Data from table Characters //////////////////////////////
	String character_sql = "select characters.charHave from characters where characters.userId=?";
	PreparedStatement pstmt_character = conn.prepareStatement(character_sql);
	pstmt_character.setString(1, userId);
	ResultSet characters = pstmt_character.executeQuery();
	
	String carHave_sql = "select car.carHave from car where car.userId=?";
	PreparedStatement pstmt_carHave = conn.prepareStatement(carHave_sql);
	pstmt_carHave.setString(1, userId);
	ResultSet haveRes = pstmt_carHave.executeQuery();
	
	String car_sql = "select car.carName from car where car.userId=? and car.carSelected=1";
	PreparedStatement pstmt_car = conn.prepareStatement(car_sql);
	pstmt_car.setString(1, userId);
	ResultSet car = pstmt_car.executeQuery();
	
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body>

<hr>
<%
	String currentCharacter=null;
	String coin = null;
	String score = null;
	
	String potion = null;
	String shield = null;
	
	String charHave = null;
	String charSelected = null;
	
	String carHave = null;
	String carSelected = null;

	while(rs.next()) {
		currentCharacter = rs.getString(1);
		coin = rs.getString(2);
		score = rs.getString(3);	
%>
<h5><%= currentCharacter %></h5>
<h5><%= coin %></h5>

<%
	String minutes_txt = null;
	String second_txt = null;
	int time_num = Integer.parseInt(score);
	int minutes_time = (time_num%3600)/60;
	int second_time = time_num%60;
	
	if (minutes_time <= 9){
	    minutes_txt = "0" + String.valueOf(minutes_time);
	}else{
	    minutes_txt = String.valueOf(minutes_time);
	}
	
	if (second_time <= 9){
	    second_txt = "0" + String.valueOf(second_time);
	}else{
	    second_txt = String.valueOf(second_time);
	}
	
	score = minutes_txt + ":" + second_txt;

%>

<h5><%= score %></h5>
<%
	}
	
	while(item_potion.next()){
		potion = item_potion.getString(1);
	}
	
	%>
	<h5><%= potion %></h5>
	
<%
	while(item_shield.next()){
		
		shield = item_shield.getString(1);
	}
%>
	<h5><%= shield %></h5>
	
<%
	while(characters.next()){
		
		charHave = characters.getString(1);
%>		
		<h5><%= charHave %></h5>
<%
	}

	while(haveRes.next()){
		
		carHave = haveRes.getString(1);
%>		
		<h5><%= carHave %></h5>
<%
	}
	
	while(car.next()){
		
		carSelected = car.getString(1);
%>		
		<h5><%= carSelected %></h5>
<%
	}
	
	rs.close();
	item_potion.close();
	item_shield.close();
	pstmt_profile.close();
	pstmt_potion.close();
	pstmt_shield.close();
	pstmt_character.close();
	pstmt_carHave.close();
	pstmt_car.close();
	conn.close();
%>
</body>
</html>