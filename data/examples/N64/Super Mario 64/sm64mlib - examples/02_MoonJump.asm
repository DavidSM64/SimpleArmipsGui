// [ARMIPS 0.9 w/ SM64 Macro Library] Moon-Jump Example by Davideesk

// When the player holds the L button, Mario will float up into the air

.orga 0x861C0 ; Set ROM address
.area 0xA4 ; Set data import limit to 0xA4 bytes
addiu sp, sp, 0xFFE8
sw ra, 0x14 (sp)

// Tests if the player is holding down the L button.
.f_testInput BUTTON_L, BUTTON_HELD, proc802CB1C0_end

li t0, SM64_MARIO_STRUCT
li t1, MARIO_ACTION_JUMP
sw t1, 0x0C(t0) ; Set mario's action to jumping.
li t1, 30.0
mtc1 t1, f2
swc1 f2, 0x4C(t0) ; Set mario's y-speed to be 30.0

proc802CB1C0_end:
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea