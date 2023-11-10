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


$poi = mysqli_query($link, "SELECT POI.ID, POI.NOME, POI.LATITUDINE, POI.LONGITUDINE, INFO.ITALIANO AS INFO, AUDIO.ITALIANO AS AUDIO, GROUP_CONCAT(FOTO.FOTO) AS GALLERIA, GROUP_CONCAT(VIDEO.VIDEO) AS VIDEOS FROM (((POI LEFT JOIN FOTO ON POI.ID = FOTO.POI) LEFT JOIN VIDEO ON POI.ID = VIDEO.POI) LEFT JOIN AUDIO ON POI.AUDIO = AUDIO.ID) LEFT JOIN INFO ON POI.INFO = INFO.ID GROUP BY POI.ID");

$json_poi = array();
$video = array();
$i = 0;

while($row = mysqli_fetch_assoc($poi))  
{  

	$json_poi[$i] = $row;
	$json_poi[$i]["GALLERIA"] = explode(",", str_replace(" ", "%", $row["GALLERIA"]));
	$video[$i] = explode(",", str_replace(" ", "%", $row["VIDEOS"]));
	$json_poi[$i]["VIDEOS"] = $video[0];

	$i = $i + 1;

}

echo json_encode($json_poi);

mysqli_close($link);

?>