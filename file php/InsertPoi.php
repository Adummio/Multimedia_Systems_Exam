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


$nomepoi = mysqli_real_escape_string($link, $_POST["nome"]);


if(!mysqli_query($link, "INSERT INTO POI(NOME, LATITUDINE, LONGITUDINE) VALUES('$nomepoi', '$_POST[latitudine]', '$_POST[longitudine]')"))
{
	echo("Error description: " . mysqli_error($link));
}

echo "Query successful.";
mysqli_close($link);

?>