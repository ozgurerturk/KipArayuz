# Kip Windows Arayüz

Kip programming UI for Windows - [Kip](https://github.com/kip-dili/kip).

# Requirements

Please refer to the installation guide for the Kip programming language in its GitHub page for acquiring kip.exe

[Kip Installation Guide](https://github.com/kip-dili/kip?tab=readme-ov-file#installation)

* You can use MSYS2 for cloning and building Foma in its environment, don't forget to build bison and flex before building Foma through MSYS2 command line (Use MSYS2 MINGW64)

* Enter the following command first and press enter to isntall all the highlighted packages

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

* After making sure that installations are done correctly, git clone the Foma, create makefile with cmake

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

* Add these environment variables PATHs, installed foma and msys2 paths may vary.

```

C:\msys64\mingw64\bin
C:\Program Files (x86)\foma\bin

```

* After installing Stack and cloning Kip, open the "stack.yaml" file in a text editor and add following commands to the end of the text to make sure Stack finds Foma files

```

extra-include-dirs:
  - 'C:\Program Files (x86)\foma\include'

extra-lib-dirs:
  - 'C:\Program Files (x86)\foma\lib'

```

* After making sure that Foma and Stack is built properly with stack.yaml file changed, stack build and install Kip, Kip should work through command line

```

cd ~\kip

stack clean
stack build
stack install

```

* Find where kip.exe is loaded into and choose it with the "Exe Yolu Seç(Find Exe Path) button.
