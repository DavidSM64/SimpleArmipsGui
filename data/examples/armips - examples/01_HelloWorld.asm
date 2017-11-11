// [ARMIPS 0.9+] "Hello World" Example by Davideesk

.defineLabel func_printXY, 0x802D66C0

.orga 0x861C0 ; Set ROM address
.area 0x90 ; Set data import limit to 0x90 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

li a2, 0x802CB250 ; Pointer to "Hello World" text
li a0, 0x60 ; x position
jal func_printXY ; call printXY function
li a1, 0x20 ; y position

lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

.orga 0x86250 ; 0x802CB250
.asciiz "Hello World"
