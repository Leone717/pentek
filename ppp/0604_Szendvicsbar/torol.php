<html lang="hu-HU">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Szendvicsbár</title>
        <link href="stílus.css" rel="stylesheet" type="text/css"/>
    </head>
    <body>
        <h1>Szendvics bár</h1>
        <nav>
            <a href="index.php"><img class="logo" src="forras/fooldal3.png" alt="főoldal"/>Főoldal</a>
            <a href="keres.php"><img class="logo" src="forras/keres2.png" alt="keres"/>Keres</a>
            <a href="torol.php"><img class="logo" src="forras/torol2.png" alt="torol"/>Töröl</a>
            <a href="feltolt.php"><img class="logo" src="forras/beszur2.png" alt="beszúr"/>Feltölt</a>
            <a href="modosit.php"><img class="logo" src="forras/fooldal2.png" alt="modosit"/>Módosít</a>
        </nav>
        <div class="mobilnav">
            <a href="index.php"><img class="logo" src="forras/fooldal3.png" alt="főoldal"/></a>
            <a href="keres.php"><img class="logo" src="forras/keres2.png" alt="keres"/></a>
            <a href="torol.php"><img class="logo" src="forras/torol2.png" alt="torol"/></a>
            <a href="feltolt.php"><img class="logo" src="forras/beszur2.png" alt="beszúr"/></a>
            <a href="modosit.php"><img class="logo" src="forras/fooldal2.png" alt="modosit"/></a>
        </div>
        <p>Válassza ki a törölni kívánt elemet</p>
        <form action="#" method="post">
            <select name="termek" id="termek">
                <?php
                include_once './aBKapcsolat.php';
                //oszlopok neveinek a lekérdezése
                $lekerdezes1 = "SHOW COLUMNS FROM kinalat";
                $oszlopok = $kapcsolat->query($lekerdezes1);
                //oszlopok száma
                $oszlopokSzama = $oszlopok->num_rows;

                //kinalat tábla lekérdezése
                $lekerdezes2 = "SELECT * FROM kinalat";
                $adatok = $kapcsolat->query($lekerdezes2)or die('Hiba a lekérdezésnél!');
                //adatok kiíratása
                //ha van legalább 1 sornyi adatom
                if ($adatok->num_rows > 0) {

                    //soronként beolvasom az adatokat
                    while ($sor2 = $adatok->fetch_row()) {
                        //elemekre bontom a sorokat, mad kiíratom
                        for ($index = 0; $index < $oszlopokSzama; $index++) {
                            if ($index == 3) 
                                {
                                echo '<option value="'.$sor2[0].'">'.$sor2[$index]." ".$sor2[1].'</option>';
                                //echo '<option value="'.$sor2[0].'">'.$sor2[$index]." ".$sor2[1].'</option>';
                            }
                        }
                    }
                }
                ?>
            </select>
            <input type="submit" name="torol" id="torol" value="Törlés">                               
        </form>
        <?php
        //törlés
        if (!empty($_POST['termek'])) {
            $id = $_POST['termek'];
            //elmentem a törölni kívánt sort
            $lekerdezes4 = "SELECT * FROM `kinalat` WHERE `termekID`=$id;";
            $adatok = $kapcsolat->query($lekerdezes4) or die('Hiba a lekérdezésnél!');

            $lekerdezes3 = "DELETE FROM `kinalat` WHERE `termekID`=$id;";
            $torles = $kapcsolat->query($lekerdezes3) or die('Hiba a törlésnél!');
            if ($torles === TRUE) {
                echo 'Sikeres adattörlés!';
                echo '<p>A törölt sor:</p>';
                if ($adatok->num_rows > 0) {
                    echo '<table>';
                    echo '<tr>';

                    while ($sor1 = $oszlopok->fetch_row()) {
                        echo "<th>";
                        echo $sor1[0];
                        echo "</th>";
                    }
                    echo '</tr>';

                    //soronként beolvasom az adatokat
                    while ($sor2 = $adatok->fetch_row()) {
                        echo '<tr>';
                        //elemekre bontom a sorokat, majd kiíratom
                        for ($index = 0; $index < $oszlopokSzama; $index++) {
                            echo "<td>";
                            echo $sor2[$index];
                            echo "</td>";
                        }
                        echo "</tr>";
                    }
                    echo "</table>";
                }
            } else {
                echo 'Sikertelen adattörlés';
            }
        }

        //bezárom a kapcsolatot
        $kapcsolat->close();
        ?>
    </body>
</html>
