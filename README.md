# Kip Windows Arayüz

An experimental tool for the Kip programming language for Windows Usage - [Kip](https://github.com/kip-dili/kip).

Please refer to Kip github page to learn about the language itself. You can try the language through Kip Playground in its page: [Kip Playground](https://kip-dili.github.io/)

# Requirements

Please refer to the installation guide for the Kip programming language in its GitHub page for acquiring kip.exe

[Kip Installation Guide](https://github.com/kip-dili/kip?tab=readme-ov-file#installation)

* You can use MSYS2 for cloning and building Foma in its environment, don't forget to build bison and flex before building Foma through MSYS2 command line (Use MSYS2 MINGW64)

[MSYS2 Download for Windows](https://www.msys2.org/)

* Enter the following command first and press enter to install all the highlighted packages

```

pacman -S --needed mingw-w64-x86_64-toolchain

```

* Then install cmake, git and other libraries 

```

pacman -S --needed mingw-w64-x86_64-cmake mingw-w64-x86_64-readline mingw-w64-x86_64-zlib git

```

* Bison and Flex installation

```

pacman -S --needed bison flex

```

* After making sure that installations are done correctly, git clone the Foma [Foma GitHub Page](https://github.com/mhulden/foma), create makefile with cmake

```

cd /c
git clone https://github.com/mhulden/foma.git
cd foma
mkdir build
cd build

cmake -G "MinGW Makefiles" ..

mingw32-make

mingw32-make install

```

* Add these environment variables PATHs, installed Foma and MSYS2 paths may vary.

```

C:\msys64\mingw64\bin
C:\Program Files (x86)\foma\bin

```

* After installing Stack and cloning Kip, locate the stack.yaml file in where Kip is cloned into, open the "stack.yaml" file in a text editor and add following commands to the end of the text to make sure Stack finds Foma files

```

extra-include-dirs:
  - 'C:\Program Files (x86)\foma\include'

extra-lib-dirs:
  - 'C:\Program Files (x86)\foma\lib'

```

* After making sure that Foma and Stack is built properly with stack.yaml file changed, stack build and install Kip through command line after opening Kip project's path, Kip should work through command line after building and installing

```

cd ~\kip

stack clean
stack build
stack install

```

* Find where kip.exe is loaded into and choose it with the "Exe Yolu Seç(Find Exe Path) button.

<img width="240" height="106" alt="image" src="https://github.com/user-attachments/assets/f6347d8e-f057-49a4-8308-0cf33560323a" />

# Usage Guide

Before starting, make sure you installed the Kip.exe successfully and manage to run it through the windows command line and then chose the kip.exe file path correctly.

Home Page

<img width="1170" height="767" alt="image" src="https://github.com/user-attachments/assets/adc043af-3e32-4403-81c3-7157cc12fcfd" />

Write your Kip code in "Kip Source" and press Run(Çalıştır) button to start the script.

<img width="1165" height="765" alt="image" src="https://github.com/user-attachments/assets/df129856-5765-47cc-a66d-216c2a354813" />

If the script expects an input, "Input" textbox in the Input Stream Group will be enabled. Enter it and send the input.

<img width="1167" height="762" alt="image" src="https://github.com/user-attachments/assets/a27717a8-cebc-4e59-b1c4-b0ce983768fe" />


File Page

<img width="1162" height="212" alt="image" src="https://github.com/user-attachments/assets/e0f5a33d-1080-4321-8700-a3f8739232f7" />

Use the File(Dosya) tab for importing or exporting .kip files.
