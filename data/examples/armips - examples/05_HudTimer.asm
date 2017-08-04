// [ARMIPS 0.9+] HUD Timer Options Example by Davideesk

// Configure Timer Options
.defineLabel TIMER_SHOW_RESET, 0x00
.defineLabel TIMER_START, 0x01
.defineLabel TIMER_PAUSE, 0x02
.defineLabel TIMER_HIDE, 0x03

// DPAD button flags
.defineLabel BTN_DRIGHT, 0x100
.defineLabel BTN_DLEFT, 0x200
.defineLabel BTN_DDOWN, 0x400
.defineLabel BTN_DUP, 0x800

// SM64 function addresses
.defineLabel func_osViBlack, 0x80323340
.defineLabel func_DMACopy, 0x80278504
.defineLabel func_ConfigureTimer, 0x802495E0

// Custom function addresses
.defineLabel func_CustomTimer, 0x80370000
.defineLabel func_CustomTimer_ROM_start, 0x7F0000
.defineLabel func_CustomTimer_ROM_end, 0x7F0100


// Call our custom function with DmaCopy from the top-most levelscript.
.orga 0x108A18
.word 0x11080000, 0x8024B940

//************** DmaCopy function **************//
.orga 0x6940 ; Overwrite the unused function 0x8024B940
.area 0x64 ; Set data import limit to 0x64 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)
sw a0, 0x10 (sp) ; lvl cmd 0x11 safeguard

// These three are nessecary because of what we overwrote at 0x108A18.
jal func_osViBlack ; call osViBlack
move a0, r0
sw r0, 0x8038BE24 ; Set level accumulator to 0

// Copies 0x100 bytes from ROM addr 0x7F0000 to RAM addr 0x80370000
la a0, func_CustomTimer ; RAM address to copy to
la a1, func_CustomTimer_ROM_start ; ROM address start to copy from
la.u a2, func_CustomTimer_ROM_end ; ROM address end point (determines copy size)
jal func_DMACopy ; call DMACopy function
la.l a2, func_CustomTimer_ROM_end ; ROM address end point (determines copy size)

lw v0, 0x10 (sp) ; lvl cmd 0x11 safeguard
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

//************** Mario loop function **************//
.orga 0x861C0 ; Set ROM address
.area 0x90 ; Set data import limit to 0x90 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

jal func_CustomTimer ; Call our custom timer function
nop

lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

//************** Custom timer function **************//
.orga func_CustomTimer_ROM_start ; Set ROM address
.area 0x100 ; Set data import limit to 0x100 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

// Check if DPAD-Right button has been pressed
lw at, 0x8033AFA0
andi at, at, BTN_DRIGHT
li t0, BTN_DRIGHT
bne at, t0, proc80370000_left
nop

jal func_ConfigureTimer ; call ConfigureTimer() function
li.l a0, TIMER_START ; start/resume timer

proc80370000_left:
// Check if DPAD-Left button has been pressed
lw at, 0x8033AFA0
andi at, at, BTN_DLEFT
li t0, BTN_DLEFT
bne at, t0, proc80370000_up
nop

jal func_ConfigureTimer ; call ConfigureTimer() function
li.l a0, TIMER_PAUSE ; pause timer

proc80370000_up:
// Check if DPAD-Up button has been pressed
lw at, 0x8033AFA0
andi at, at, BTN_DUP
li t0, BTN_DUP
bne at, t0, proc80370000_down
nop

jal func_ConfigureTimer ; call ConfigureTimer() function
li.l a0, TIMER_SHOW_RESET ; show and reset timer

proc80370000_down:
// Check if DPAD-Down button has been pressed
lw at, 0x8033AFA0
andi at, at, BTN_DDOWN
li t0, BTN_DDOWN
bne at, t0, proc80370000_end
nop

jal func_ConfigureTimer ; call ConfigureTimer() function
li.l a0, TIMER_HIDE ; show and reset timer

proc80370000_end:
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

