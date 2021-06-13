
<html lang="hu-HU">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Szendvicsbár</title>
        <link href="stílus.css" rel="stylesheet" type="text/css"/>
    </head>
    <body>
        <?php
        include_once './abKapcsolat.php';
        ?>
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
        <?php
        $lekerdezes1 = "SHOW COLUMNS FROM kinalat";
        $oszlopok = $kapcsolat->query($lekerdezes1);
        $oszlopokSzama = $oszlopok->num_rows;
        
        $lekerdezes2 = "SELECT * FROM kinalat";
        $adatok = $kapcsolat->query($lekerdezes2) or die('Hiba a lekérdezésnél');
        
        if ($adatok->num_rows > 0) {
            echo '<table>';
            echo '<tr>';
            
            $szamlalo = 0;
            while ($sor1 = $oszlopok->fetch_row()){
            if ($szamlalo == 1 || $szamlalo == 2 || $szamlalo ==3 || $szamlalo == 4) {
                echo "<th>";
                echo $sor1[0];
                echo "</th>";
            }
            
            $szamlalo++;
            
          }
          echo '</tr>';
          
          while ($sor2 = $adatok->fetch_row()) {
              echo '<tr>';
              for ($index =0; $index < $oszlopokSzama; $index++) {
                  if ($index == 1 || $index == 2 || $index == 3)
                  {
                      echo "<td>";
                      echo $sor2[$index];
                      echo "</td>";
                  }
                  if ($index == 4)
                  {
                      echo "<td>";
                      echo '<img class="logo" src="szendvicsek/'.$sor2[$index].'" alt="' .$sor2[$index].'"/>';
                      echo "</td>";
                  }
              }
              echo '</tr>';
          }
          echo '</table>';
        }
        else 
        {
            echo "Nincsenek adatok a kínálat táblában!";
        }
        
        $kapcsolat->close();
        ?>
    </body>
</html>
