# Game-Tarjo-the-Explorer

# Daftar isi

- [Penamaan Asset](#penamaan-asset)
  - [Folder](#folder)
    - [Folder Debug](#folder-debug)
  - [Source Code](#source-code)
  - [Non-Code Assets](#non-code-assets)
    - [Persistent/Important GameObjects](#persistent-important-gameobjects)
    - [Debug Objects](#debug-objects)
- [Directory/File Structure](#directory-file-structure)
  - [Assets](#assets)
  - [Scripts](#scripts)
  - [Models](#models)
- [Workflow](#workflow)
  - [Models](#models-1)
  - [Sprite/2D Asset](#sprite-2d-asset)
  - [File Konfigurasi](#file-konfigurasi)
  - [Localization](#localization)
  - [Audio](#audio)

<small><i><a href='http://ecotrust-canada.github.io/markdown-toc/'>Table of contents generated with markdown-toc</a></i></small>

# Penamaan Asset

Pertama, dan paling utama, tidak boleh ada spasi di nama file atau folder

## Folder

`PascalCase`

Lebih baik banyak sub-folder daripada harus menamai asset dengan nama yang panjang.

Nama folder harus sesingkat mungkin, satu atau dua kata. Kalau namanya dilihat terlalu panjang, bisa jadi foldernyya bisa dipisah ke beberapa sub-folder.

Coba untuk punya satu tipe file per folder. Harusnya `Textures/Trees`, `Models/Trees` bukan `Trees/Textures`, `Trees/Models`. Dengan begitu, jadi lebih mudah untuk mengatur root folder untuk macam macam software lain.

Untuk kategorisasi asset yang berhubungan dengan environment atau art style, pakai tipe dasar asset untuk parent folder nya, misalnya `Trees/Jungle`, `Trees/City` bukan `Jungle/Trees`, `City/Trees`. Agar lebih mudah membandingkan style antara asset dengan tipe yang sama.

### Folder Debug

`[PascalCase]`

Asset debug, yang tidak masuk ke produk final. Misalnya, debug folder `[Assets]` dan folder biasa `Assets`.

## Source Code

Pakai naming convention dari bahasa pemrograman nya. Untuk C# dan shader files pakai `PascalCase`, sesuai convention C#.

## Non-Code Assets

Pakai `tree_small` not `small_tree`. lebih mudah mengelompokkan kumpulan objek tree daripada kumpulan objek small.

Pakai `camelCase` kalau dibutuhkan. Pakai `weapon_miniGun` bukan `weapon_gun_mini`. Tapi sebisa mungkin dihindari, misalnya, `vehicles_fighterJet` harusnya `vehicles_jet_fighter` jika ada banyak tipe jet.

Lebih baik pakai sufiks yang deskriptif daripada iteratif: `vehicle_truck_damaged` jangan `vehicle_truck_01`. Jika memang pakai angka untuk sufiks, selalu tulis dalam format 2 digit. Dan **JANGAN** gunakan sebagai versioning system, karena itu sudah diatur oleh git.

### Persistent/Important GameObjects

`_snake_case`

Gunakan underscore didepan nama objek untuk objek yang tidak spesifik di satu scene agar lebih mudah dibedakan.

### Debug Objects

`[SNAKE_CASE]`

Seperti debug folder, objek yang hanya dipakai untuk debuggin/testing, dan tidak masuk ke produk final, dipakaikan kurung siku.

# Struktur File/Folder

```
Root
├── Assets
├── Build
└── Tools           # Programs to aid development: compilers, asset managers etc.
```

## Assets

```
Assets
├── Art
│   ├── Materials
│   ├── Models			# 3D Model, FBX and BLEND files
│   ├── Sprites			# PNG files
│   └── Textures
├── Audio
│   ├── Music
│   └── Sound
├── Code
│   └── Scripts			# C# scripts
├── Docs						# Wiki, Concept art, Marketing material
├── Level						# Anything related to game design in Unity
│   ├── Prefabs
│   └── Scenes
│   └── UI
├── Resources       # Configuration files, localization text and other user files.
└── ThirdParty			# Assets from third party sources, i.e. Asset store
```

## Scripts

Gunakan namespace sesuai dengan struktur folder.

Direktori Framework bagus untuk kode yang dapat digunakan kembali di projek lain.

Isi folder Scripts bisa bervariasi tergantung pada proyek, tapi, `Environment`, `Framework`, `Tools`, dan `UI` harus, kurang lebih sama di seluruh proyek.

```
Scripts
├── Environment
├── Framework
├── NPC
├── Player
├── Tools
└── UI
```

## Models

Pisahkan file model dari modelling program dan file model yang sudah diexport.

```
Models
├── Blend
└── FBX
```

# Workflow

## Models

File extension: `FBX`

Meskipun Unity mendukung file Blender secara default, lebih baik memisahkan file yang sedang dikerjakan dan file yang sudah diekspor. Ini juga perlu dilakukan saat menggunakan software lain, misalnya Substance untuk texturing.

Export Settings: `Y up`, `-Z forward`, `uniform scale`.

## Sprite/2D Asset

File extension: `PNG`

## File Konfigurasi

File extension: `JSON`

Gunakan format file binary untuk file yang tidak boleh diubah oleh pemain.

## Localization

File extension: `CSV`

Banyak digunakan oleh localization software, lebih mudah untuk mengedit string menggunakan spreadsheet.

## Audio

File extension: `WAV` while mixing, `OGG` in game.

Preload small sound clips to memory, load on the fly for longer music and less frequent ambient noise files.

# Be Consistent

> The point of having style guidelines is to have a common vocabulary of coding so people can concentrate on what you're saying rather than on how you're saying it. We present global style rules here so people know the vocabulary, but local style is also important. If code you add to a file looks drastically different from the existing code around it, it throws readers out of their rhythm when they go to read it. Avoid this.
> -- [Google C++ Style Guide](https://google.github.io/styleguide/cppguide.html)

---
