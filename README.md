# BallTalk

## Sistemos paskirtis

Projekto tikslas – leisti krepšinio fanų bendruomenei dalintis savo mintimis apie krepšinio aktualijas tarpusavyje.

Veikimo principas – pačią kuriamą platformą sudaro dvi dalys: internetinė aplikacija bei aplikacijų programavimo sąsaja (angl. trump. API). 

Žmogus, norėdamas naudotis šia platforma turės prisiregistruoti prie sistemos. Kiekvienas registruotas vartotojas galės įkelti įrašą. Kiti sistemos naudotojai galės už įrašą balsuoti „patinka“ arba „nepatinka“ bei pakomentuoti įrašą. Taip pat sistema turės administratoriaus rolę, turės patvirtinti viešai platinamus įrašus, galės peržiūrėti paviešintas įrašus ir trinti kitų žmonių komentarus.

## Funkciniai reikalavimai

**Neregistruotas sistemos naudotojas galės:**
- Peržiūrėti platformos reprezentacinį puslapį
- Prisijungti/prisiregistruoti prie internetinės aplikacijos

**Registruotas sistemos naudotojas galės:**
- Atsijungti nuo internetinės aplikacijos
- Prisijungti (užsiregistruoti) prie platformos
- Pridėti įrašą:
    - Pridėti įrašo pavadinimą
    - Pridėti įrašo aprašymą
- Paskelbti savo įrašą
- Peržiūrėti kitų narių įrašus
- Komentuoti kitų narių įrašus
- Ištrinti ar redaguoti savo komentarus;
- Peržiūrėti įrašų komentarus

**Administratorius galės:**
- Patvirtinti ar įrašas gali būti viešinamas
- Peržiūrėti visus paviešintus įrašus
- Trinti netinkamus įrašus
- Trinti netinkamus komentarus

## Sistemos architektūra

**Sistemos sudedamosios dalys:**
- Kliento pusė (ang. Front-End) – naudojant React.js
- Serverio pusė (angl. Back-End) – naudojant ASP>NET Core. Duomenų bazė – SQL server

![Picture1](https://user-images.githubusercontent.com/66777570/194723730-37666f03-8a72-4d8c-a33a-3e3e5ceb2e91.png)

##API specification
