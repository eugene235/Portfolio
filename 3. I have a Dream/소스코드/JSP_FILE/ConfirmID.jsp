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
	
	String sql = "select * from user where user.userId=?";
	PreparedStatement pstmt = conn.prepareStatement(sql);
	pstmt.setString(1, userId);
	ResultSet rs = pstmt.executeQuery();

%>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body>

<%

	if(rs.next()){	
%>
		<h5>exist</h5>
<%
	}
	else{
%>
		<h5>success</h5>
<%
	}

	rs.close();
	pstmt.close();
	conn.close();
%>

</body>
</html>