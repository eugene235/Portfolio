<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
    
<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	
	String userId = request.getParameter("userId");
	String potion_num = request.getParameter("potion");
	String shield_num = request.getParameter("shield");
	String s_coin = request.getParameter("coin");
	String s_score = request.getParameter("score");
	
	int p_count = Integer.parseInt(potion_num);
	int s_count = Integer.parseInt(shield_num);
	int coin = Integer.parseInt(s_coin);
	int score = Integer.parseInt(s_score);
	int selected_coin = 0;
	int selected_score = 0;
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	String select_coin = "select profile.coin, profile.score from profile where profile.userId=?";
	PreparedStatement pstmt_select = conn.prepareStatement(select_coin);
	pstmt_select.setString(1, userId);
	ResultSet rs = pstmt_select.executeQuery();
	
	while(rs.next()){
		selected_coin = rs.getInt(1);
		selected_score = rs.getInt(2);
	}
	
	coin += selected_coin;
	
	String update_1 = "update Quiz set isSet=false where quizIndex=?";
	PreparedStatement pstmt_update_1 = conn.prepareStatement(update_1);
	pstmt_update_1.setInt(1, 1);
	pstmt_update_1.execute();
	
	String update_2 = "update Quiz set isSet=false where quizIndex=?";
	PreparedStatement pstmt_update_2 = conn.prepareStatement(update_2);
	pstmt_update_2.setInt(1, 2);
	pstmt_update_2.execute();
	
	String update_3 = "update Quiz set isSet=false where quizIndex=?";
	PreparedStatement pstmt_update_3 = conn.prepareStatement(update_3);
	pstmt_update_3.setInt(1, 3);
	pstmt_update_3.execute();
	
	String update_4 = "update Quiz set isSet=false where quizIndex=?";
	PreparedStatement pstmt_update_4 = conn.prepareStatement(update_4);
	pstmt_update_4.setInt(1, 4);
	pstmt_update_4.execute();
	
	String update_5 = "update Quiz set isSet=false where quizIndex=?";
	PreparedStatement pstmt_update_5 = conn.prepareStatement(update_5);
	pstmt_update_5.setInt(1, 5);
	pstmt_update_5.execute();
	
	String update_potion = "update potion set potion.pCount=? where potion.userId=? and potion.pName='potion'";
	PreparedStatement pstmt_potion = conn.prepareStatement(update_potion);
	pstmt_potion.setInt(1, p_count);
	pstmt_potion.setString(2, userId);
	pstmt_potion.execute();
	
	String update_shield = "update potion set potion.pCount=? where potion.userId=? and potion.pName='shield'";
	PreparedStatement pstmt_shield = conn.prepareStatement(update_shield);
	pstmt_shield.setInt(1, s_count);
	pstmt_shield.setString(2, userId);
	pstmt_shield.execute();
	
	String update_coin = "update profile set profile.coin=? where profile.userId=?";
	PreparedStatement pstmt_coin = conn.prepareStatement(update_coin);
	pstmt_coin.setInt(1, coin);
	pstmt_coin.setString(2, userId);
	pstmt_coin.execute();
	
	if (score!= 0 && score < selected_score){
		String update_score = "update profile set profile.score=? where profile.userId=?";
		PreparedStatement pstmt_score = conn.prepareStatement(update_score);
		pstmt_score.setInt(1, score);
		pstmt_score.setString(2, userId);
		pstmt_score.execute();
		pstmt_score.close();
	}
	
	pstmt_select.close();
	
	pstmt_update_1.close();
	pstmt_update_2.close();
	pstmt_update_3.close();
	pstmt_update_4.close();
	pstmt_update_5.close();
	
	pstmt_potion.close();
	pstmt_shield.close();
	pstmt_coin.close();

	rs.close();
	conn.close();

%>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body>

</body>
</html>