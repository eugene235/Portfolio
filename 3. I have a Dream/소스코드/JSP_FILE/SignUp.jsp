<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<%@ page import="java.sql.*" %>

<%
	request.setCharacterEncoding("utf-8");
	String userId = request.getParameter("userId");
	String pwd = request.getParameter("pwd");
	
	Class.forName("com.mysql.jdbc.Driver");
	String url = "jdbc:mysql://localhost:3306/gamedb";
	String user = "Eugene";
	String password = "20131131";
	
	Connection conn = DriverManager.getConnection(url, user, password);
	
	
	String sql = "insert into user (userId, pwd) values (?, ?)";
	//String sql = "select userId from profileTable where profileTable.userId=?";
	//String sql = "insert into profile (userId, pwd, currentCharacter, coin, score) values (?, ?, 'ch1', 0, 0)";
	PreparedStatement pstmt = conn.prepareStatement(sql);
	pstmt.setString(1, userId);
	pstmt.setString(2, pwd);
	pstmt.execute();
	
	String sql0 = "insert into profile (userId, coin, score) values (?, 0, 0)";
	PreparedStatement pstmt0 = conn.prepareStatement(sql0);
	pstmt0.setString(1, userId);
	pstmt0.execute();
	
	//insert initial value
	String sql1 = "insert into potion (userId, pName, pCount) values (?, 'potion', 0)";
	pstmt = conn.prepareStatement(sql1);
	pstmt.setString(1, userId);
	pstmt.execute();
	
	String sql2 = "insert into potion (userId, pName, pCount) values (?, 'shield', 0)";
	pstmt = conn.prepareStatement(sql2);
	pstmt.setString(1, userId);
	pstmt.execute();
	
	
	
	String sql3 = "insert into car (userId, carName, carHave, carSelected) values (?, 'airballoon', 1, 1)";
	pstmt = conn.prepareStatement(sql3);
	pstmt.setString(1, userId);
	pstmt.execute();
	
	String sql4 = "insert into car (userId, carName, carHave, carSelected) values (?, 'rocket', 0, 0)";
	pstmt = conn.prepareStatement(sql4);
	pstmt.setString(1, userId);
	pstmt.execute();
	
	String sql5 = "insert into car (userId, carName, carHave, carSelected) values (?, 'ufo', 0, 0)";
	pstmt = conn.prepareStatement(sql5);
	pstmt.setString(1, userId);
	pstmt.execute();
	
	
	
	String sql6 = "insert into characters (userId, charName, charHave, charSelected) values (?, 'ch1', 1, 1)";
	pstmt = conn.prepareStatement(sql6);
	pstmt.setString(1, userId);
	pstmt.execute();
	
	String sql7 = "insert into characters (userId, charName, charHave, charSelected) values (?, 'ch2', 0, 0)";
	pstmt = conn.prepareStatement(sql7);
	pstmt.setString(1, userId);
	pstmt.execute();
	
	String sql8 = "insert into characters (userId, charName, charHave, charSelected) values (?, 'ch3', 0, 0)";
	pstmt = conn.prepareStatement(sql8);
	pstmt.setString(1, userId);
	pstmt.execute();


		
	String confirm = "select user.userId, user.pwd from user where user.userId=? and user.pwd=?";
	pstmt = conn.prepareStatement(confirm);
	pstmt.setString(1, userId);
	pstmt.setString(2, pwd);
	ResultSet rs = pstmt.executeQuery();

%>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body>

<%
	String idConfirm = null;
	String pwConfirm = null;
	
	while(rs.next()) {
		if (userId.equals(rs.getString(1)) && pwd.equals(rs.getString(2))){
%>
			<h5>success</h5>
<% 
			return;
		}
		
	}
	rs.close();
	pstmt.close();
	conn.close();
%>

<h5>fail</h5>

</body>
</html>