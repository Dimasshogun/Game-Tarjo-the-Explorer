# Writing guide Dialogue

File script dialog ditulis dalam file berformat `.yarn` di disimpan di dalam folder [`Resources/Dialog`](../Resources/Dialog) dan dikelompokkan berdasarkan level dimana dialog tersebut dipakai

Disarankan menggunakan [Yarn Editor](https://github.com/YarnSpinnerTool/YarnEditor) untuk menulis script dialog karena sintaks dan terminologi nya sama dengan yang dipakai di project. Bisa pakai [Twine](https://twinery.org) tapi silahkan pelajari sendiri perbedaan sintaks dan terminologi nya.

Guide ini dipersingkat dan difokuskan kepada pemakaian yang spesifik ke project ini, untuk full guide nya bisa dilihat di halaman [Official Yarn Spinner Documentation](https://yarnspinner.dev/docs/writing/)

Satu set dialog dikelompokkan dalam sebuah `Node`. Satu node berisi satu konteks percakapan saja, jika berbeda konteks lebih baik dipisah ke node baru. Misalnya, karakter Ucup memiliki percakapan general di dalam node `Ucup` dan memiliki percakapan untuk quest di dalam node `UcupQuest`.

# Penjelasan sintaks

## Line

Yang ditampilkan sebagai dialog yang diucapkan karakter.
```
Nama Karakter: Apa yang dikatakannya
```
Setiap line akan ditampilkan secara berurutan per baris.

## Options

Untuk menampilkan option pada dialog jika ditekan player makan akan menjalankan node yang disebutkan.
```
[[Teks Option|NamaNode]]
```

Normalnya, option dipakai untuk berpindah node. Tapi penggunaan option bisa dipersingkat dengan shortcut option,
```
-> Teks Option
    Karakter: Apa katanya?
```
dan untuk berpindah node bisa dilakukan dengan Jump, sitaks nya seperti option normal tapi hanya menyebutkan nama node nya saja.
```
[[NamaNode]]
```

Perlu diingat, walaupun shortcut lebih mudah dipakai tetap harus memperhatikan konteks dan kompleksitas dari dialog nya, apabila sudah masuk ke konteks lain atau percakapan menjadi terlalu kompleks tetap harus memakai sintaks option biasa.

Lengkapnya bisa dilihat di halaman [Controlling Dialogue](https://yarnspinner.dev/docs/writing/controlling/) di dokumentasi yarn spinner.


## Commands

Digunakan untuk mentrigger method-method tertentu melalui dialog

Yarn spinner memberikan dua built-in command `wait` untuk pause dialog selama waktu yang ditentukan (dalam satuan detik) dan `stop` untuk langsung keluar dari dialog. Diluar itu harus buat sendiri.

secara umum, sintaks untuk command adalah sebagai berikut
``` 
<<namaCommand GameObject Parameter dipisah oleh spasi>>
```

Command yang ada di project ini (akan terus diupdate sepanjang proses development)

### Quest

Command-command yang berhubungan dengan quest system. Didefinisikan di class [Quest.QuestManager](../Code/Scripts/Quest/QuestManager.cs).

- startQuest, untuk memulai quest dari NPC
  ```
  <<startQuest QuestCode>>
  ```
- advanceQuestStage, untuk maju ke index stage quest yang disebutkan
  ```
  <<advanceQuestStage QuestCode stage>>
  ```
  
### Minigame

Command-command yang berhubungan dengan minigame. Didefinisikan di class [Minigame.MinigameController](../Code/Scripts/Minigame/MinigameController.cs).

- startMinigame, untuk memulai minigame. Command ini menunda dialogue sampai minigame diselesaikan
  ```
  <<startMinigame NamaPrefabMinigame>>
  ```
  
### Cutscene

Command-command yang berhubungan dengan cutscene. Didefinisikan di class [Cutscene.CutsceneController](../Code/Scripts/Cutscene/CutsceneController.cs) dan juga membutuhkan prefab [CutsceneManager](../Level/Prefabs/Manager/CutsceneManager.prefab) dan objek-objek pendukung cutscene, lihat dokumentasi setup objek-objek cutscene [disini](./howTo_cutscene.md).

- moveCharacter, untuk menggerakkan karakter ke satu titik tertentu
  ```
  <<moveCharacter NamaKarakter NamaTarget metodeGerak>>
  ```
  `metodeGerak` memiliki dua opsi:
  - `teleport`, untuk memindahkan karakter secara instan, dan 
  - `walk`, untuk membuat karakter 'berjalan'.
- switchCamera, untuk 'mengganti' sudut pandang kamera dengan cara pindah ke instance `Cinemachine Virtual Camera` yang ditentukan.
  ```
  <<switchCamera NamaKamera MetodeTransisi>>
  ```
  `MetodeTransisi` dibuat berdasarkan class [`CinemachineBlendDefinition.Style`](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.3/api/Cinemachine.CinemachineBlendDefinition.Style.html?q=cinemachineblen), tapi baru ada dua opsi sudah diimplementasi; `Cut` dan `EaseIn`.
  
- switchToPlayerCamera, setelah selesai berpindah-pindah kamera didalam cutscene, pakai command ini untuk kembali ke kamera yang megikuti player.
  ```
  <<switchToPlayerCamera>>
  ```
  
### NPC

Command-command yang berhubungan dengan kontrol NPC. Didefinisikan di class [Character.CharacterManager](../Code/Scripts/Character/CharacterManager.cs). Memerlukan komponen skrip [Character.CharacterInstance](../Code/Scripts/Character/CharacterInstance.cs) didalam objek karakter untuk bisa bekerja (default di prebaf Player dan NPC).

- animateCharacter, untuk memainkan animasi pada sebuah karakter di dalam dialogue.
  ```
  <<animateCharacter NamaKarakter AnimationType updateIdle>>
  ```
  `AnimationType` merujuk kepada definisi nama Animation State di animator controller dari masing-masing karakter. Tipe animasi yang bisa diputar saat ini:
  - `Idle`
  - `Speak`
  - `Cry`
  - `Happy`
  - `Wave`
  
  Jika menyebut tipe yang diluar tipe-tipe tersebut akan default ke `Idle`.
 
  `updateIdle` flag yang jika ditulis menandakan apakah animasi idle harus diupdate ke animasi yang akan dijalankan atau tidak. Seharusnya dipakai sebagai default state ketika karakter tidak sedang bicara, tapi sekarang animasi bicara masih belum diimplementasi jadi masih belum berfungsi s epenuhnya.

### Cutscene UI

Command-command yang berhubungan dengan cutscene UI. Didefinisikan di class [Cutscene.UICutsceneController](../Code/Scripts/Cutscene/UICutsceneController.cs) dan membutuhkan prefab [CutsceneManager](../Level/Prefabs/Manager/CutsceneManager.prefab).

- playTransition, untuk menjalankan transisi screen fade
  ```
  <<playTransition AnimationState>>
  ```
  `AnimationState` merujuk kepada definisi nama Animation State di animator controller [UICutscene](../Art/Animation/UICutscene.controller). 

  Beberapa transisi yang sudah dibuat:
  - `FadeToBlack`, transisi ke layar full hitam
  - `FadeToWhite`, transisi ke layar full putih. bisa untuk menampilkan satu gambar jika komponen `Image` di objek `Overlay` diberi diisi variabel Source Image nya.

- resetTransition, untuk mereset state animator controller. Biasanya dipanggil ketika selesai transisi.
  ```
  <<resetTransition>>
  ```
  
- toggleDialogueUI, untuk menyembunyikan/memunculkan UI dialogue.
  ```
  <<toggleDialogueUI true/false>>
  ```

- showBigText, untuk memunculkan teks besar ditengah layar (belum bekerja dengan baik).
  ```
  <<showBigText "teks besar">>
  ```

Guide lebih detail tentang penggunaan dan cara membuat custom command bisa dilihat di halaman [Working With Commands](https://yarnspinner.dev/docs/unity/working-with-commands/) di dokumentasi yarn spinner.

## Function

Seperti namanya, Function didefinisikan untuk bisa digunakan didalam yarn script. Biasanya function digunakan dengan [Expression dan if statement](https://yarnspinner.dev/docs/writing/expressions-and-variables/#expressions-and-if-statements)

Function yang ada di project ini (akan terus diopdate sepanjang proses development)

- questStage, menerima parameter string `questCode` dan integer `stageIndex`, untuk mengecek apakah sedang ada di stage quest tertentu.
  ```
  questStage("QuestCode", stageIndex)
  ```

- isQuestComplete, menerima parameter string `questCode`, untuk mengecek apakah quest sudah selesai dengan status `Success`.
  ```
  isQuestComplete("QuestCode")
  ```

Cara membuat function bisa dilihat dokumentasi untuk [DialogueRunner.AddFunction](https://yarnspinner.dev/api/yarn.unity/dialoguerunner/yarn.unity.dialoguerunner.addfunctionsystem.stringsystem.int32yarn.returningfunction/)

Referensi penulisan Yarn script di project ini bisa lihat,
- file [Level1/Alia.yarn](../Resources/Dialog/Level1/Alia.yarn), untuk contoh penggunaan command di kategori quest
- file [Level1/Ibu.yarn](../Resources/Dialog/Level1/Ibu.yarn), untuk contoh penggunaan command di kategori cutscene
- file [Level1/School.yarn](../Resources/Dialog/Level1/School.yarn), untuk contoh penggunaan command di kategori UI cutscene dan contoh penggunaan function isQuestComplete

> Perhatian: Saat penulisan di Yarn Editor jika ingin Yarn script nya bisa dicoba dijalankan langsung, jangan dulu menggunakan Command dan Function yang ada di project. Sebagai alternatif bisa gunakan fitur variables yang diberikan oleh Yarn Spinner sebagai placeholder.

## Memasukkan dialogue ke dalam scene

Untuk memasukkan dialogue ke dalam scene ada dua hal yang harus dilakukan,

Pertama, tambahkan dialog ke `DialogueManager` untuk diload saat scene di play.

![Dialogue Manager Inspector](Images/howTo_dialogue_DialogueManager_filled.png)

Kedua, setup NPC,
- Ambil prefab `NPC` di folder [`Level/Prefabs`](../Level/Prefabs) masukkan kedalam scene, terserah secara langsung atau ke Hierarchy.
- Buka GameObject NPC di Inspector

  ![NPC Inspector](Images/howTo_dialogue_NPCInspector.png)

- Rename GameObject dan isi `Character Name` sesuai nama karakter,

  Isi `Entry Node` dengan nama node yang ingin dieksekusi ketika memulai percakapan dengan NPC, biasanya sama dengan nama NPC

  Didalam prefab `NPC` sudah ada script `NpcQuest`, script ini digunakan untuk manage quest marker yang tampak diatas NPC. Jika NPC tidak punya quest script ini bisa diremove atau dinonaktifkan.

  Berikut contoh gameobject `NPC` yang sudah terisi

  Setelah semua lengkap, ketika dijalankan game nya dan kita menghampiri NPC tersebut maka player bisa berinteraksi dengan nya, ditandai dengan munculnya tombol Interact.
 
  ![Contoh NPC Inspector](Images/howTo_dialogue_NPCInspector_filled.png)

