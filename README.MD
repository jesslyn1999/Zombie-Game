# Morty and The Cronenberg

## Deskripsi Permainan
Pemain awalnya mengisi username terlebih dahulu sebelum memulai permainan. Saat permainan dimulai, Pemain memiliki batas nyawa tertentu. Tujuan dari permainan yaitu membunuh musuh sebanyak mungkin. Setiap membunuh jenis musuh yang berbeda, pemain akan mendapat nilai yang berbeda pula. Pemain dapat melihat score di ujung kiri atas jendela dan nilai pada ujung kanan atas. Pemain mati apabila nyawa habis. Pemain dapat pula melihat scoreboard yang pemain dengan username berbeda-beda.

## Cara kerja
Permainan ini menggunakan teknis kerja Object Pooling baik dalam peluru dan enemy spawner. Musuh di setting akan selalu muncul pada border kanan kamera virtual yang selalu mengikuti player. Selain itu, terdapat satu jenis musuh pula yang digerakkan berdasarkan teknik AI. Teknik ini diterapkan melalui free package yang disediakan di Unity sendiri.

## Libraries
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json),
- [Cinemachine](https://unity.com/unity/features/editor/art-and-design/cinemachine),
- [A* Pathfinding Project](https://arongranberg.com/astar/),
- dan bawaan Library Unity sendiri.

## Screenshots
![Main Menu](/screenshots/MainMenu.PNG "Tampilan Utama")
![Input Username](/screenshots/InputUsername.PNG "Masukkan username")
![Play Scene](/screenshots/PlayScene.PNG "Play Kuy !")
![Play Scene 2](/screenshots/PlayScene_2.PNG "Play Kuy !!")
![Dead Scene](/screenshots/DeadScene.PNG "Die lahh")
![Scoreboard](/screenshots/Scoreboard.PNG "Cek peringkat")