<?php

$servername = "localhost";
$username = "id9733406_adummio";
$password = "ApaMondaSpaziani";
$dbName = "id9733406_ams_db";

//Make connection
$link = mysqli_connect($servername, $username, $password, $dbName);

//Check connection
if (!$link) {
    echo "Error: Unable to connect to MySQL." . PHP_EOL;
    echo "Debugging errno: " . mysqli_connect_errno() . PHP_EOL;
    echo "Debugging error: " . mysqli_connect_error() . PHP_EOL;
    exit;
}


$result = mysqli_query($link, "SELECT * FROM POI WHERE NOME = '$_POST[nomepoi]'");

if(mysqli_num_rows($result) == 0)
{
	echo("-1");
}
else
{
	$value = mysqli_fetch_object($result)->ID;
	echo $value;
}


mysqli_close($link);

?>