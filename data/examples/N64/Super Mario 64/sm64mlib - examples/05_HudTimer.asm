// [ARMIPS 0.9 w/ SM64 Macro Library] HUD Timer Options Example by Davideesk

// Configure Timer Options
.defineLabel TIMER_SHOW_RESET, 0x00
.defineLabel TIMER_START, 0x01
.defineLabel TIMER_PAUSE, 0x02
.defineLabel TIMER_HIDE, 0x03

// Custom function addresses
.defineLabel func_CustomTimer, 0x80370000
.defineLabel func_CustomTimer_ROM_start, 0x7F0000
.defineLabel func_CustomTimer_ROM_end, 0x7F0100

// Call our custom function with DmaCopy from the top-most levelscript.
.orga 0x108A18
.ls_callFunc 0x00, 0x8024B940

//************** DmaCopy function **************//
.orga 0x6940 ; Overwrite the unused function 0x8024B940
.area 0x64 ; Set data import limit to 0x64 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)
sw a0, 0x10 (sp) ; lvl cmd 0x11 safeguard

// These two are nessecary because of what we overwrote at 0x108A18.
.f_osViBlack FALSE ; Set screen blackout to false
sw r0, 0x8038BE24 ; Set level accumulator to 0

// Copies 0x100 bytes from ROM addr 0x7F0000 to RAM addr 0x80370000
.f_DmaCopy func_CustomTimer, func_CustomTimer_ROM_start, func_CustomTimer_ROM_end

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
.f_testInput BUTTON_DRIGHT, BUTTON_PRESSED, proc80370000_left
.f_ConfigureTimer TIMER_START ; start/resume timer

proc80370000_left:
// Check if DPAD-Left button has been pressed
.f_testInput BUTTON_DLEFT, BUTTON_PRESSED, proc80370000_up
.f_ConfigureTimer TIMER_PAUSE ; pause timer

proc80370000_up:
// Check if DPAD-Up button has been pressed
.f_testInput BUTTON_DUP, BUTTON_PRESSED, proc80370000_down
.f_ConfigureTimer TIMER_SHOW_RESET ; show and reset timer

proc80370000_down:
// Check if DPAD-Down button has been pressed
.f_testInput BUTTON_DDOWN, BUTTON_PRESSED, proc80370000_end
.f_ConfigureTimer TIMER_HIDE ; hide timer

proc80370000_end:
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

