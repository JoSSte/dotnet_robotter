# Robotter

Jonas Stevnsvig

## Beslutninger

### Overordnet

1. Erstat M (move?) med F for forward for mere letlæselighed, samt mulighed for R (Reverse) i fremtiden
1. Koordinatsystem er ikke (nødvendigvis) unikt for én robot
1. Support for flere kommandoer
1. Dokumentation er på engelsk

### Koordinater

* Minimum bræt er 2x2

### Robotter

* ...

## Antagelser

1. Destination på hver kommando beregnes før bevægelse
2. Drej er ikke en bevægelse. DVS startposition (2,2,E) efterfulgt af kommandoen RRR vil være (2,2,N)
3. Maks størrelse på koordinatsystem er unsigned integer (uint: 0 <= x,y <= 4 294 967 295)
4. Ugyldige kommandoer parses ved start og giver en fejl inden man flytter (fx: `(1,2,N)` med kommando `FF` resulterer i "`(1,2,N)` Ugyldig bevægelse" fremfor "`(1,1,N)` Ugyldig bevægelse" - forskel er om man laver hele trækket inden. )
   * Da der kun er en robot, og ingen forhindringer er dette mere effektivt end at foretrække et træk og så se om det næste er gyldigt
