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

$nomepoi = mysqli_real_escape_string($link, $_POST["nomepoi"]);

$poi = $link->query("SELECT * FROM POI WHERE NOME = '$nomepoi'");

if(mysqli_num_rows($poi) != 0)
{
	$fetchedpoi = mysqli_fetch_object($poi);
	$id = $fetchedpoi->ID;
	$info = $fetchedpoi->INFO;
	$audio = $fetchedpoi->AUDIO;

	if(!is_null($info))
		$link->query("DELETE FROM INFO WHERE ID = '$info'");


	if(!is_null($audio))
	{
		$audioquery = $link->query("SELECT * FROM AUDIO WHERE ID = '$audio'");

		$fetchedaudio = mysqli_fetch_object($audioquery);
		$italiano = $fetchedaudio->ITALIANO;
		$inglese = $fetchedaudio->INGLESE;
		$francese = $fetchedaudio->FRANCESE;

		$link->query("DELETE FROM AUDIO WHERE ID = '$audio'");
		if(!is_null($italiano))
			unlink("audio/" . $italiano);
		if(!is_null($inglese))
			unlink("audio/" . $inglese);
		if(!is_null($francese))
			unlink("audio/" . $francese);
	}

	$fotoquery = $link->query("SELECT * FROM FOTO WHERE POI = '$id'");

	if(mysqli_num_rows($fotoquery) != 0)
	{
		$link->query("DELETE FROM FOTO WHERE POI = '$id'");

		while($row = mysqli_fetch_assoc($fotoquery))
		{
			unlink("foto/" . $row["FOTO"]);
		}

	}

	$videoquery = $link->query("SELECT * FROM VIDEO WHERE POI = '$id'");

	if(mysqli_num_rows($videoquery) != 0)
	{
		$link->query("DELETE FROM VIDEO WHERE POI = '$id'");

		while($row = mysqli_fetch_assoc($videoquery))
		{
			unlink("video/" . $row["VIDEO"]);
		}
	}

	$link->query("DELETE FROM POI WHERE ID = '$id'");

	echo "Query successful.";
}
else
{
	echo("Poi not found");
}

mysqli_close($link);

?>