// [ARMIPS 0.9+] DmaCopy Example by Davideesk

// SM64 function addresses
.defineLabel func_osViBlack, 0x80323340
.defineLabel func_DMACopy, 0x80278504
.defineLabel func_printXY, 0x802D66C0

// Addresses to the custom text
.defineLabel ascii_HelloDMACopy, 0x80370000
.defineLabel ascii_HelloDMACopy_ROM_start, 0x7F0000
.defineLabel ascii_HelloDMACopy_ROM_end, 0x7F0010

// Call our custom function with DMACopy from the top-most levelscript.
.orga 0x108A18
.word 0x11080000, 0x8024B940

// Text to be displayed on screen
.orga ascii_HelloDMACopy_ROM_start
.ascii "-Hello DmaCopy-"
.byte 0x00 ; add Null terminator to end of string

//************** Copy data from ROM into RAM **************//
.orga 0x6940 ; Overwrite the unused function 0x8024B940
.area 0x64 ; Set data import limit to 0x64 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)
sw a0, 0x10 (sp) ; lvl cmd 0x11 safeguard

// These three are nessecary because of what we overwrote at 0x108A18.
jal func_osViBlack ; call osViBlack
move a0, r0
sw r0, 0x8038BE24 ; Set level accumulator to 0

// Copies 0x10 bytes from ROM addr 0x7F0000 to RAM addr 0x80370000
la a0, ascii_HelloDMACopy ; RAM address to copy to
la a1, ascii_HelloDMACopy_ROM_start ; ROM address start to copy from
la.u a2, ascii_HelloDMACopy_ROM_end ; ROM address end point (determines copy size)
jal func_DMACopy ; call DMACopy function
la.l a2, ascii_HelloDMACopy_ROM_end ; ROM address end point (determines copy size)

lw v0, 0x10 (sp) ; lvl cmd 0x11 safeguard
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

//************** Display Text on screen **************//
.orga 0x861C0 ; Set ROM address
.area 0xA4 ; Set data import limit to 0xA4 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

// Prints "-Hello DmaCopy-" at the screen pos (0x70, 0x14)
la a2, ascii_HelloDMACopy ; Pointer to "-Hello DmaCopy-"
li a0, 0x70 ; x position
jal func_printXY ; call printXY function
li.l a1, 0x14 ; y position

lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea
