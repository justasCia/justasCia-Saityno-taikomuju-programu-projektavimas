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

## Naudotojo sąsajos projektas

### Prisijungimo lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208941960-99dc7286-c3af-4886-acfe-e9b0dfe46b66.png)
![image](https://user-images.githubusercontent.com/66777570/208942076-c0725950-e759-4db5-93c4-ea05ea69227c.png)

### Registracijos lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208942204-49493492-5755-4df4-95f4-b5da35a1b04e.png)
![image](https://user-images.githubusercontent.com/66777570/208942286-9ebcc484-cdf7-4d20-a58f-f029692e80de.png)

### Temų peržiūros lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208942404-abd00a0b-ce07-4cbe-88b8-d293cac1b3a1.png)
![image](https://user-images.githubusercontent.com/66777570/208942536-c39467ce-0512-42b2-b4af-abac6977b189.png)

### Temos pridėjimo lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208942735-0d220754-538a-4119-957a-2dd9ab7b6d79.png)
![image](https://user-images.githubusercontent.com/66777570/208942803-487aed97-7ac5-42d4-bb94-4fadc051ae6c.png)


## API specification

**GET /topics**
