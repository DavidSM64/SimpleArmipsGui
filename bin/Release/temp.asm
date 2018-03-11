SAG_FILEPATH equ "C:\Users\David\Desktop\sm64 - Copy (2).z64"
SAG_FILEPOS equ 0x0
SAG_IMPORTPATH equ "C:\Users\David\Desktop\sm64 - Copy (2).z64"
.Open SAG_IMPORTPATH, SAG_FILEPOS
// This code will run BEFORE the libraries have loaded.

// Set armips to N64 mode
.n64

.include "C:\Users\David\Documents\C#\armipsSimpleGui\armipsSimpleGui\bin\Release/data/profiles/N64\libs\sm64mlib\sm64mlib.asm"
// This code will run AFTER the libraries have loaded.
.include "C:\Users\David\Desktop\sm64Optimized\sm64Bars_SubmarineTest\FPScounter.asm"
.Close
