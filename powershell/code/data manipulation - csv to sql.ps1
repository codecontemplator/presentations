
$conn = New-Object Data.SqlClient.SqlConnection 'connection string'
$cmd = $conn.CreateCommand()
$cmd.CommandText = "INSERT INTO MyTable VALUES (@Name, @Age)"

Import-Csv .\data.csv | Foreach-Object {
	$cmd.Parameters.Clear()
	$cmd.Parameters.AddWithValue("@Name",$_.Name)
	$cmd.Parameters.AddWithValue("@Age",$_.Age)	
	$cmd.ExecuteNonQuery()
}
					
