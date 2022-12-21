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

### Temos įrašų peržiūros lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208943394-3a7867a6-ccb5-4418-849a-ec6fac7bbd28.png)
![image](https://user-images.githubusercontent.com/66777570/208943438-a63c032f-ee53-4a3a-95fd-8f87dbf23164.png)

### Įrašo pridėjimo lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208943639-c7c273d7-6f6a-4cfe-aa04-a768019599dd.png)
![image](https://user-images.githubusercontent.com/66777570/208943662-64728f70-42fd-42de-8067-0097659a3771.png)

### Įrašo bei jo komentarų peržiūros lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208943843-bab5e097-9e66-4ced-b00f-880814adf20d.png)
![image](https://user-images.githubusercontent.com/66777570/208943898-af7d1b70-c061-4e02-b45e-8d06eead331f.png)

### Komentaro pridėjimo lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208943998-ac2bb619-efcf-4de6-a0be-fc266ebaed05.png)
![image](https://user-images.githubusercontent.com/66777570/208944047-b8cd2389-5292-4e06-a6e6-afbe4e737b05.png)

### Komentaro redagavimo lango wireframe ir realizacija sistemoje

![image](https://user-images.githubusercontent.com/66777570/208944219-1526010f-a16b-492f-bf24-012ca69e891e.png)
![image](https://user-images.githubusercontent.com/66777570/208944270-796514e2-18fb-4845-8d57-7e2fee831e9f.png)

### Likusių langų ir modalinių langų realizacija sistemoje:

![image](https://user-images.githubusercontent.com/66777570/208944586-805788a8-a78f-4b29-b7d6-9c1aa8de12cd.png)
![image](https://user-images.githubusercontent.com/66777570/208944650-29dbf56c-e2b6-4b3f-924b-abfbdc7e9956.png)
![image](https://user-images.githubusercontent.com/66777570/208944686-948c2f7a-6432-49a2-a007-744537343513.png)




## API specification

**GET /topics**
