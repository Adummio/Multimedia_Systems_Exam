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

$tabella = $_POST["tabella"];

$result = $link->query("SELECT * FROM $tabella WHERE ID = '$_POST[id]'");

$value = mysqli_fetch_row($result)->$tabella;

if(!$link->query("DELETE FROM $tabella WHERE ID = '$_POST[id]'"))
{
	echo("Error description: " . mysqli_error($link));
}
else
{
	unlink(strtolower($tabella) . "/" . $value);
	echo "Query successful.";
}
mysqli_close($link);

?>