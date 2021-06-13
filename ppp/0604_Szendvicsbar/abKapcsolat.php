<?php

$host="localhost";
$fn="root";
$jsz="";
$ab="szendvicsbar0604";

$kapcsolat = new mysqli($host, $fn, $jsz, $ab) or die('Hiba az adatbázishoz csatlakozáskor!');

//Magyar ékezetes betűk jól működése
$kapcsolat->query("SET NAMES UTF8");
$kapcsolat->query("set character set UTF8");
$kapcsolat->query("set collation_connection='utf8_hungary_ci'");
        

