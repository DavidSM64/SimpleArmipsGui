// [ARMIPS 0.9+] Change Mario Colors Example by Davideesk

// When the player presses the L button, Mario's clothes will change color. 
// Mario will cycle through 5 colors (Red -> Green -> Blue -> Purple -> Yellow -> Red)

// Button flags
.defineLabel BTN_L, 0x20

// Misc
.defineLabel NUMBER_COLORS, 0x5

// SM64 function addresses
.defineLabel func_osViBlack, 0x80323340
.defineLabel func_DMACopy, 0x80278504

// Custom RAM addresses
.defineLabel func_SetMarioColor, 0x80370000
.defineLabel func_MarioColorLoop, 0x80370020
.defineLabel colorIndex, 0x802CB260
.defineLabel colorsLocation, 0x80370120

// Custom ROM addresses
.defineLabel func_SetMarioColor_ROM_start, 0x7F0000
.defineLabel func_MarioColorLoop_ROM_start, 0x7F0020
.defineLabel colorsLocation_ROM_start, 0x7F0120
.defineLabel data_ROM_end, 0x7F0200


// Call our custom function with DMACopy from the top-most levelscript.
.orga 0x108A18
.word 0x11080000, 0x8024B940

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
la a0, func_SetMarioColor ; RAM address to copy to
la a1, func_SetMarioColor_ROM_start ; ROM address start to copy from
la.u a2, data_ROM_end ; ROM address end point (determines copy size)
jal func_DMACopy ; call DMACopy function
la.l a2, data_ROM_end ; ROM address end point (determines copy size)

lw v0, 0x10 (sp) ; lvl cmd 0x11 safeguard
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

//************** Mario loop function **************//
.orga 0x861C0 ; Set ROM address (RAM Address: 0x802CB1C0)
.area 0xA0, 0xFF ; Set data import limit to 0xA0 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

jal func_MarioColorLoop ; Call our custom loop function
nop

lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

// Resereve space for our color index (Address: 0x802CB260)
.word 0x00000000

//******************* Custom Functions *******************//

//****** func_SetMarioColor ******//
.orga func_SetMarioColor_ROM_start
.area 0x20, 0xFE
lui t0, 0x8008
sd a0, 0xEC40 (t0)
sd a1, 0xEC38 (t0)
sd a2, 0xEC28 (t0)
jr ra
sd a3, 0xEC20 (t0)
.endarea

//****** func_MarioColorLoop ******//
.orga func_MarioColorLoop_ROM_start ; Set ROM address
.area 0x100, 0xFF ; Set data import limit to 0x100 bytes, and fill empty space with 0xFF
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

// Tests if the player pressed the L button
lw at, 0x8033AFA0
andi at, at, BTN_L
li a0, BTN_L
bne at, a0, func_MarioColorLoop_end
nop

la t0, colorsLocation
lw t1, colorIndex
add t0, t0, t1

ld a0, 0x00(t0)
ld a1, 0x08(t0)
ld a2, 0x10(t0)
ld a3, 0x18(t0)

jal func_SetMarioColor
nop

lw t2, colorIndex
slti t3, t2, (NUMBER_COLORS - 1) * 0x20
beqz t3, func_MarioColorLoop_makeZero
nop
addiu t2, t2, 0x20
sw t2, colorIndex
b func_MarioColorLoop_end
nop
func_MarioColorLoop_makeZero:
sw r0, colorIndex
func_MarioColorLoop_end:
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

// Now were making our own custom macros!
.macro .marioColor, hatColorLight, hatColorDark, overallsColorLight, overallsColorDark
	.word hatColorLight, hatColorLight, hatColorDark, hatColorDark
	.word overallsColorLight, overallsColorLight, overallsColorDark, overallsColorDark
.endmacro

// Mario Colors, 0x20 bytes each.
.orga colorsLocation_ROM_start
.marioColor 0x00FF00FF, 0x007F00FF, 0x0000FFFF, 0x00007FFF // Green Mario
.marioColor 0x0000FFFF, 0x00007FFF, 0xFF0000FF, 0x7F0000FF // Blue Mario
.marioColor 0x7F00FFFF, 0x3F007FFF, 0x00004FFF, 0x00001FFF // Purple Mario
.marioColor 0xFFFF00FF, 0x7F7F00FF, 0xAF00AFFF, 0x4F004FFF // Yellow Mario
.marioColor 0xFF0000FF, 0x7F0000FF, 0x0000FFFF, 0x00007FFF // Red Mario (original)

