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
        <p>Válassza ki a módosítani kívánt elemet</p>
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
        //módosítás
        ?>
        <form action="#" method="post">
            <label for="nev">Szendvics azonosítója:</label>
            <select name="azon" id="azon">
                <?php
                include_once './abKapcsolat.php';

                $lekerdezes1 = "SHOW COLUMNS FROM kinalat";
                $oszlopok = $kapcsolat->query($lekerdezes1);
                $oszlopokSzama = $oszlopok->num_rows;

                $lekerdezes2 = "SELECT * FROM kinalat";
                $adatok = $kapcsolat->query($lekerdezes2) or die('Hiba a lekérdezésnél!');

                if ($adatok->num_rows > 0) {
                    //echo '<table>';
                    //echo '<tr>';


                    while ($sor2 = $adatok->fetch_row()) {

                        for ($index = 0; $index < $oszlopokSzama; $index++) {
                            if ($index == 0) {
                                echo '<option value="' . $sor2[$index] . '">' . $sor2[$index] . '</option>';
                            }
                        }
                    }
                }
                ?>
            </select>
            <label for="nev">Szendvics neve:</label>
            <input type="text" name="nev" id="nev" required>
            <label for="jszam">Szendvics ára:</label>
            <input type="text" name="ar" id="ar" required>
            <label for="telefon">Csomagolás</label>
            <select name="csomag" id="csomag" required>
                <option selected value="0">Nincs csomagolás</option>
                <option value="1">Van csomagolás</option>
            </select>
            <label for="kep">Kép</label>
            <input type="text" name="kep" id="kep" required>
            <input type="submit" name="ujSzendvics" id="ujSzendvics" value="Szendvics módosítása">

        </form>
                    <!-- <form action="<?php /* echo $_SERVER['PHP_SELF'] */ ?>" method='post'>
                        <label>Szendvics nevazon:</label>
                        <select name="azon" id="azon">
        <?php
        /* while($sor=$adatok->fetch_row())
          {
          echo "<option value=\"$sor[3]\">$sor[3]</option>";
          } */
        ?>
                        </select > 
                        <label>Státusz:</label>
                        <select name="statusz" id="statusz">
                            <option value='0'>Nem csomagolt</option>
                            <option value='1'>Csomagolt</option>
                            
                        </select>
                        <input type="submit" name="rogzit" id="rogzit" value="Módosít">
                    </form>-->
        <?php
        if (!empty($_POST['ujSzendvics'])) {
            //$adatbazis->modosit();
            $azon = $_POST['azon'];

            function ell($adat) {
                $adat = trim($adat);
                $adat = stripcslashes($adat);
                $adat = htmlspecialchars($adat);
                return $adat;
            }

            //itt a 0 csomagértéket üresnek veszi így érdems 1, 2-vel dolgozni! !empty($_POST['csomag'])
            if (!empty($_POST['nev']) && !empty($_POST['ar']) && !empty($_POST['kep'])) {
                $nev = ell($_POST['nev']);
                $ar = ell($_POST['ar']);
                $csomag = ell($_POST['csomag']);
                $kep = ell($_POST['kep']);

                //ismétlődő név ellenőrzése feltöltéskor SELECT * FROM `kinalat` WHERE nev = 'Bagel' "SELECT * FROM `kinalat` WHERE `nev` ='" + $nev + "'";
                $nevmegnez = "SELECT * FROM `kinalat` WHERE `nev` LIKE '%$nev%'";
                $ismetlodoNev = $kapcsolat->query($nevmegnez) or die("Hiba a név ellenőrzésekor!");

                if (mysqli_num_rows($ismetlodoNev) == 0) {
                    //var_dump($ismetlodoNev);
                    echo "Nincs még adatbázisban<br><br>";
                    $sql = "UPDATE `kinalat` SET `ar`=\"$ar\", `csomagolas`=\"$csomag\", `nev`=\"$nev\",`kep`=\"$kep\" WHERE `termekID`=\"$azon\"";
                    //echo $sql;
                    $sql = "UPDATE `kinalat` SET `ar`=\"$ar\", `csomagolas`=\"$csomag\", `nev`=\"$nev\",`kep`=\"$kep\" WHERE `termekID`=\"$azon\"";
                    //echo $sql;
                    $modosit = $kapcsolat->query($sql);

                    if ($modosit === TRUE) {
                        echo "Sikeres adatmódosítás";

                        echo "<p>A módosított sor:</p>";
                        //$keresett=$_GET['szoveg'];

                        $lekerdezes1 = "SHOW COLUMNS FROM kinalat";
                        $oszlopok = $kapcsolat->query($lekerdezes1);
                        $oszlopokSzama = $oszlopok->num_rows;


                        //$lekerdezes2 = "SELECT * FROM `kinalat` WHERE `nev` LIKE '%$keresett%'";

                        $lekerdezes2 = "SELECT * FROM `kinalat` WHERE `termekID` LIKE '%$azon%'";
                        $adatok = $kapcsolat->query($lekerdezes2)or die('Hiba a lekérdezésnél!');


                        if ($adatok->num_rows > 0) {
                            echo '<table>';
                            echo '<tr>';


                            while ($sor1 = $oszlopok->fetch_row()) {

                                echo "<th>";
                                echo $sor1[0];
                                echo "</th>";
                            }
                            echo '</tr>';

                            while ($sor2 = $adatok->fetch_row()) {
                                echo '<tr>';
                                for ($index = 0; $index < $oszlopokSzama; $index++) {
                                    if ($index == 1 || $index == 2 || $index == 3) {
                                        echo "<td>";
                                        echo $sor2[$index];
                                        echo "</td>";
                                    }
                                    if ($index == 4) {
                                        echo "<td>";
                                        echo '<img class="logo" src="szendvicsek/' . $sor2[$index] . '" alt="' . $sor2[$index] . '"/>';
                                        echo "</td>";
                                    }
                                }
                                echo '</tr>';
                            }
                            echo '</table>';
                        } else {
                            echo "Nincs találat!";
                        }
                    }
                } else {
                    echo "Sikertelen adatmódosítás.";
                }
            } else {
                echo 'Sikertelen módosítás! Már van az adatbázisban!';
            }
        }

        //bezárom a kapcsolatot
        $kapcsolat->close();
        ?>
    </body>
</html>