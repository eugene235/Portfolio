<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
    
<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	//String userId = request.getParameter("userId");
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	//String sql = "insert into profileTable (userId, pwd) values (?, ?)";
	String sql = "select characters.charName, profile.userId, profile.coin, profile.score from profile, characters where profile.userID = characters.userID and characters.charSelected=1 and profile.score!=0 order by profile.score";
	PreparedStatement pstmt = conn.prepareStatement(sql);
	//pstmt.setString(1, userId);
	ResultSet rs = pstmt.executeQuery();

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
	String userId=null;
	String coin = null;
	String score = null;
	
	while(rs.next()) {
		currentCharacter = rs.getString(1);
		userId = rs.getString(2);
		coin = rs.getString(3);
		score = rs.getString(4);	
%>
<h5><%= currentCharacter %></h5>
<h5><%= userId %></h5>
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
	rs.close();
	pstmt.close();
	conn.close();
%>


</body>
</html>