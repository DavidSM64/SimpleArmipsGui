// [ARMIPS 0.9 w/ SM64 Macro Library] "Sweet Dreams" Example by Davideesk

// A 1-up mushroom will spawn when Mario falls asleep
// Based off of this tutorial: http://origami64.net/showthread.php?tid=413

.defineLabel ACTION_SLEEP, 		0x0C000203
.defineLabel BEHAVIOR_1UP, 		0x130040EC

.orga 0x861C0 ; Set ROM address
.area 0xA4 ; Set data import limit to 0xA4 bytes
addiu sp, sp, 0xFFE8
sw ra, 0x14 (sp)
lui t0, 0x8034

// Test for Mario sleeping, branch to end if false.
.f_TestForMarioAction ACTION_SLEEP, proc802CB1C0_end 

lw t3, 0xB1D0(t0) ; Load word at 0x8033B1D0 (For this example it's a timer)
li t4, 0x20  ; Sleep delay (how much time before mushroom spawns)
bne t3, t4, proc802CB1C0_incrementTimer
nop

// Spawn a 1-up mushroom at Mario's location.
.f_SpawnObj SM64_CURR_OBJ_PTR, 0xD4, BEHAVIOR_1UP 

li t4, 20.0  ; 20.0 = 0x41A00000
mtc1 t4, f0 ; Move value at T4 to float register F0
lwc1 f2, 0xA4(v0) ; Get Y position of 1-up mushroom
add.s f2, f2, f0 ; Add 20.0 to Y position of 1-up mushroom
swc1 f2, 0xA4(v0) ; Store new Y position of 1-up mushroom

proc802CB1C0_incrementTimer:
addiu t3, t3, 0x01
sw t3, 0x8033B1D0

proc802CB1C0_end:
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea