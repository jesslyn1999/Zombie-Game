# Morty and The Cronenberg

## Deskripsi Permainan
Pemain awalnya mengisi username terlebih dahulu sebelum memulai permainan. Saat permainan dimulai, Pemain memiliki batas nyawa tertentu. Tujuan dari permainan yaitu membunuh musuh sebanyak mungkin. Setiap membunuh jenis musuh yang berbeda, pemain akan mendapat nilai yang berbeda pula. Pemain dapat melihat score di ujung kiri atas jendela dan nilai pada ujung kanan atas. Pemain mati apabila nyawa habis. Pemain dapat pula melihat scoreboard yang pemain dengan username berbeda-beda.

## Features
### Wajib
- Terdapat karakter (https://www.gameart2d.com/freebies.html) pada game ini. Karakter dapat berjalan, menembakan senjata, dan lompat. Pergerakan karakter harus mengikuti hukum fisika (collider, rigidbody).
- Karakter digerakan dengan menggunakan keyboard. 
- Terdapat sound effect pada saat karakter menembak.
- Pergerakan kamera mengikuti pergerakan karakter.
- Terdapat animasi pada pergerakan karakter. Berikan animasi karakter pada saat bergerak, menembak, dan loncat. 
- Generate zombie yang merupakan musuh. Jika pemain bersentuhan dengan zombie, maka darah pemain berkurang. Lokasi zombie muncul pada arah <i>player</i>.
- Karakter awalnya mempunyai darah bernilai 20 dan dapat di setting melalui player prefs. Permainan berhenti ketika darah pemain mencapai 0.
- Design dari peta permainan cukup kreatif.
- Game menyimpan score dari pemain. Pemain mendapatkan score ketika membunuh lawan. Tampilan score tersebut ada pada layar permainan.
- Score disimpan pada suatu basis data online. Data scoreboard diperoleh menggunakan HTTP request dan dapat dilakukan update sesuai username yang diinput di awal permainan. Server scoreboard untuk melakukan operasi ini disediakan oleh asisten PBD.
- Selain scene permainan, terdapat juga scene Main Menu dan Scoreboard yang memanfaatkan object canvas dan layout panel untuk kedua scene ini.
- Menggunakan PlayerPrefs untuk menyimpan setting permainan, seperti audio di-enable atau tidak.
- Assets yang digunakan dalam format 2D. Karakter dan musuh telah dikembangkan lebih lanjut oleh developer.
- Game dibuat dalam aplikasi desktop.

### Bonus
- Pemain dapat menggunakan dua senjata yaitu pedang dan peluru.
- Ada dua jenis musuh yang muncul dalam permainan yaitu musuh berbasis AI dan non AI.
- Permainan ini menggunakan teknis kerja Object Pooling baik dalam peluru dan enemy spawner. Musuh telah di-setting akan selalu muncul pada border kanan kamera virtual yang selalu mengikuti player.

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