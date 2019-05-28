<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
    
<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	int rand_num = (int) (Math.random() * 5) + 1;
	
	String q = "select * from quiz where quizIndex=?";
	PreparedStatement pstmt_quiz = conn.prepareStatement(q);
	pstmt_quiz.setInt(1, rand_num);
	ResultSet rs = pstmt_quiz.executeQuery();
	
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body>

<%
	String quiz = null, answer = null, wrong_1 = null, wrong_2 = null;
	boolean isSet = false;
	int rand_num2 = 0;

	while(rs.next()){
		quiz = rs.getString(2);
		answer = rs.getString(3);
		wrong_1 = rs.getString(4);
		wrong_2 = rs.getString(5);
		isSet = rs.getBoolean(6);
	}
	
	while(isSet){
		
		rand_num2 = (int) (Math.random() * 5) + 1;
		String qq = "select * from quiz where quizIndex=?";
		PreparedStatement pstmt_quiz2 = conn.prepareStatement(qq);
		pstmt_quiz2.setInt(1, rand_num2);
		ResultSet rs2 = pstmt_quiz2.executeQuery();
		
		while(rs2.next()){
			quiz = rs2.getString(2);
			answer = rs2.getString(3);
			wrong_1 = rs2.getString(4);
			wrong_2 = rs2.getString(5);
			isSet = rs2.getBoolean(6);
		}
		rs2.close();
		pstmt_quiz2.close();	
	}
		
	rs.close();
	pstmt_quiz.close();
	
	%>

	<h5><%=quiz%></h5>
	<h5><%=answer%></h5>
	<h5><%=wrong_1%></h5>
	<h5><%=wrong_2%></h5>
	
	<%	
	
	String update = "update Quiz set isSet=true where quizIndex=?";
	PreparedStatement pstmt_update = conn.prepareStatement(update);
	pstmt_update.setInt(1, rand_num);
	
	PreparedStatement pstmt_update2 = conn.prepareStatement(update);
	pstmt_update2.setInt(1, rand_num2);
	
	pstmt_update.execute();
	pstmt_update2.execute();
		
	pstmt_update.close();
	conn.close();
%>

</body>
</html>