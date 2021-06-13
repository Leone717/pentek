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
        <p> Keresés a kínálat táblában: </p>
        <form action="#" method="get">
            <select name="szoveg" id="szoveg">
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
                            if ($index == 3) {
                                echo '<option value="' . $sor2[$index] . '">' . $sor2[$index] . '</option>';
                            }
                        }
                    }
                }
                ?>
            </select>
            <!--<input name = "szoveg" id="szoveg">-->
            <input type="submit" name="keres" id="keres2" valze="Keres">
        </form>
        <?php
        if (isset($_GET['keres'])) {
            echo "<p>Azon sorok, melyekben megtalálható a keresett kifejezés:</p>";
            $keresett=$_GET['szoveg'];

            $lekerdezes1 = "SHOW COLUMNS FROM kinalat";
            $oszlopok = $kapcsolat->query($lekerdezes1);
            $oszlopokSzama = $oszlopok->num_rows;

            
            //$lekerdezes2 = "SELECT * FROM `kinalat` WHERE `nev` LIKE '%$keresett%'";

            $lekerdezes2 = "SELECT * FROM `kinalat` WHERE `nev` LIKE '%$keresett%'";
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

        $kapcsolat->close();
        ?>

    </body>
</html>
