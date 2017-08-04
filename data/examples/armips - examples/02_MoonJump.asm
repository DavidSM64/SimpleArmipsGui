// [ARMIPS 0.9+] Moon-Jump Example by Davideesk

// When the player holds the L button, Mario will float up into the air

.defineLabel BTN_L, 0x20
.defineLabel MARIO_STRUCT, 0x8033B170

// See this wiki page for a list of Mario's actions: http://wiki.origami64.net/sm64:actions
.defineLabel ACTION_JUMP, 0x03000880 

.orga 0x861C0 ; Set ROM address, we are overwritting a useless loop function as our hook.
.area 0xA4 ; Set data import limit to 0xA4 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

// Tests if the player is holding down the L button.
lh at, 0x8033AFA0
andi at, at, BTN_L
li a0, BTN_L
bne at, a0, proc802CB1C0_end
nop

li t0, MARIO_STRUCT
li t1, ACTION_JUMP
sw t1, 0x0C(t0) ; Set mario's action to jumping.
li t1, 30.0
mtc1 t1, f2
swc1 f2, 0x4C(t0) ; Set mario's y-speed to be 30.0

proc802CB1C0_end:
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

