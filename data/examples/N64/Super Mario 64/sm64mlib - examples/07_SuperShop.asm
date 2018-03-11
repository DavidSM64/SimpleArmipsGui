// [ARMIPS 0.9+ w/ SM64 Macro Library] Super Shop Example by Davideesk

// When the player presses the L button, a HUD shop will appear allowing Mario to buy stuff.
// The player can buy 5 items: Wing Cap, Metal Cap, Vanish Cap, Star Shell, and 1-Up.
// While the shop is open, the player can look through the list of items by pressing
// DPAD-Left or DPAD-Right. The player will pay for the item with DPAD-DOWN if they
// have enough coins to spend.

// Model IDs
.defineLabel modelID_WingCap, 0x87
.defineLabel modelID_MetalCap, 0x86
.defineLabel modelID_VanishCap, 0x88
.defineLabel modelID_Star, 0x7A
.defineLabel modelID_1UP, 0xD4

// Behaviors
.defineLabel behavior_WingCap, 0x13003DB8
.defineLabel behavior_MetalCap, 0x13003DD8
.defineLabel behavior_VanishCap, 0x13003E1C
.defineLabel behavior_Shell, 0x13001F3C
.defineLabel behavior_1UP, 0x13003FDC

// Item Costs
.defineLabel cost_WingCap, 15
.defineLabel cost_MetalCap, 20
.defineLabel cost_VanishCap, 10
.defineLabel cost_StarShell, 15
.defineLabel cost_1UP, 40

// Misc
.defineLabel totalNumOfShopItems, 5
.defineLabel marioObject, 0x80361158

// SM64 function addresses
.defineLabel func_spawnOBJ, 0x8029EDCC

// Custom function addresses
.defineLabel func_SuperShopLoop, 0x80370000
.defineLabel func_ShopCheckBuyItem, 0x80370200

// Custom ROM addresses
.defineLabel func_SuperShopLoop_ROM_start, 0x7F0000
.defineLabel data_ROM_end, 0x7F0340

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
.f_osViBlack FALSE ; Set screen blackout to false
sw r0, 0x8038BE24 ; Set level accumulator to 0

// Copies 0x10 bytes from ROM addr 0x7F0000 to RAM addr 0x80370000
.f_DmaCopy func_SuperShopLoop, func_SuperShopLoop_ROM_start, data_ROM_end

lw v0, 0x10 (sp) ; lvl cmd 0x11 safeguard
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

//************** Mario loop function **************//
.orga 0x861C0 ; Set ROM address (RAM Address: 0x802CB1C0)
.area 0x20, 0xFF ; Set data import limit to 0x20 bytes
addiu sp, sp, -0x18
sw ra, 0x14 (sp)

jal func_SuperShopLoop ; Call our custom shop loop function
nop

lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

//******************* ASCII Text & Shop Variables *******************//
.orga 0x861E0
.defineLabel text_SuperShop, 0x802CB1E0
.area 0x10, 0x00
.ascii "Super Shop"
.endarea
// The main reason for calling the vanish cap "Ghost Cap" is due to the
// North American version of the game missing the 'V' character in the GUI text.
// We can easily add the 'V' back in, but that is a tutorial for another time.
.defineLabel text_VanishCap, 0x802CB1F0
.area 0x10, 0x00
.ascii "Ghost Cap"
.endarea
.defineLabel text_MetalCap, 0x802CB200
.area 0x10, 0x00
.ascii "Metal Cap"
.endarea
.defineLabel text_WingCap, 0x802CB210
.area 0x10, 0x00
.ascii "Wing Cap"
.endarea
.defineLabel text_StarShell, 0x802CB220
.area 0x10, 0x00
.ascii "Star Shell"
.endarea
.defineLabel text_1UP, 0x802CB230
.area 0x10, 0x00
.ascii "1up"
.endarea
.defineLabel text_CoinAmount, 0x802CB240
.area 0x10, 0x00
.ascii "+%d"
.endarea

.defineLabel shouldShowShop, 0x802CB250
.byte 0x00
.defineLabel shopItem, 0x802CB251
.byte 0x00


//******************* Custom Functions & Macros *******************//

.macro .printShopItem, text_name, coin_amount_imm
	.f_PrintXY 0x48, 0x80, text_name
	.f_PrintImm 0x38, 0x70, text_CoinAmount, coin_amount_imm
.endmacro

//**** func_SuperShopLoop ****//
.orga func_SuperShopLoop_ROM_start ; Set ROM address
.area 0x200, 0xFF ; Set data import limit to 0x200 bytes, and fill empty space with 0xFF
addiu sp, sp, -0x18
sw ra, 0x14 (sp)
sw s0, 0x10 (sp) ; Store s0 in stack, so we can use it in the function.
lui s0, 0x802D

// Tests if the player pressed the L button
.f_testInput BUTTON_L, BUTTON_PRESSED, func_SuperShopLoop_showShop

lbu t0, 0xB250(s0)
xori t0, t0, 0x1 ; XOR is used to toggle shouldShowShop between 0 and 1
sb t0, 0xB250(s0)

func_SuperShopLoop_showShop:
lbu t0, 0xB250(s0)
beqz t0, func_SuperShopLoop_end
nop

// Tests if the player pressed the DPAD-Left button
func_SuperShopLoop_testLeft:
.f_testInput BUTTON_DLEFT, BUTTON_PRESSED, func_SuperShopLoop_testRight

lbu t0, 0xB251(s0)
beqz t0, func_SuperShopLoop_testRight
nop
subiu t0, t0, 1
sb t0, 0xB251(s0) ; if shopItem > 0, then subtract value by 1

// Tests if the player pressed the DPAD-Right button
func_SuperShopLoop_testRight:
.f_testInput BUTTON_DRIGHT, BUTTON_PRESSED, func_SuperShopLoop_shopGUI

lbu t0, 0xB251(s0)
li t1, totalNumOfShopItems - 1
beq t0, t1, func_SuperShopLoop_shopGUI
nop
addiu t0, t0, 1 ; if shopItem < (totalNumOfShopItems - 1), then add value by 1
sb t0, 0xB251(s0)

func_SuperShopLoop_shopGUI:
.f_PrintXY 0x48, 0xA0, text_SuperShop

lbu t0, 0xB251(s0)
li t1, 0x1
beq t0, t1, func_SuperShopLoop_showMetalCap
nop
li t1, 0x2
beq t0, t1, func_SuperShopLoop_showVanishCap
nop
li t1, 0x3
beq t0, t1, func_SuperShopLoop_showStarShell
nop
li t1, 0x4
beq t0, t1, func_SuperShopLoop_show1UP
nop

func_SuperShopLoop_showWingCap:
.printShopItem text_WingCap, cost_WingCap
b func_SuperShopLoop_checkBuy
nop

func_SuperShopLoop_showMetalCap:
.printShopItem text_MetalCap, cost_MetalCap
b func_SuperShopLoop_checkBuy
nop

func_SuperShopLoop_showVanishCap:
.printShopItem text_VanishCap, cost_VanishCap
b func_SuperShopLoop_checkBuy
nop

func_SuperShopLoop_showStarShell:
.printShopItem text_StarShell, cost_StarShell
b func_SuperShopLoop_checkBuy
nop

func_SuperShopLoop_show1UP:
.printShopItem text_1UP, cost_1UP

func_SuperShopLoop_checkBuy:
jal func_ShopCheckBuyItem
nop

func_SuperShopLoop_end:
lw s0, 0x10 (sp) ; restore s0 to what it was before.
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea

.macro .setupBuyItem, modelID, behaviorAddress, subtractCoinAmount
	lw a0, marioObject ; We want to spawn the item at Mario's location
	li a1, modelID
	la a2, behaviorAddress
	b func_ShopCheckBuyItem_buyItem
	li.l s0, subtractCoinAmount
.endmacro

.macro .buyItem
	// Note: 0x8033B218 = Number coins Mario has, and 0x8033B262 = Number coins in HUD display
	lui t0, 0x8034
	lh t1, 0xB218(t0) ; Get number of coins mario has
	subu t1, t1, s0 ; Subtract coin value by item cost
	slti t2, t1, 0x00 ; Check to see if subtracted value is smaller than zero
	bnez t2, func_ShopCheckBuyItem_end
	nop
	sh t1, 0xB218(t0) ; Store new amount of coins
	sb r0, shouldShowShop ; Hide the shop GUI after purchase.
	jal func_spawnOBJ ; Call function to spawn new item.
	sh t1, 0xB262(t0) ; Store new amount of coins to HUD display
.endmacro

//****** func_ShopCheckBuyItem ******//
.area 0x140, 0xFF
addiu sp, sp, -0x18
sw ra, 0x14 (sp)
sw s0, 0x10 (sp) ; Save s0 in stack so we can use it in the function

.f_testInput BUTTON_DDOWN, BUTTON_PRESSED, func_ShopCheckBuyItem_end

lbu t0, shopItem
li t1, 0x1
beq t0, t1, func_ShopCheckBuyItem_buyMetal
nop
li t1, 0x2
beq t0, t1, func_ShopCheckBuyItem_buyVanish
nop
li t1, 0x3
beq t0, t1, func_ShopCheckBuyItem_buyStarShell
nop
li t1, 0x4
beq t0, t1, func_ShopCheckBuyItem_buy1UP
nop

func_ShopCheckBuyItem_buyWing:
.setupBuyItem modelID_WingCap, behavior_WingCap, cost_WingCap

func_ShopCheckBuyItem_buyMetal:
.setupBuyItem modelID_MetalCap, behavior_MetalCap, cost_MetalCap

func_ShopCheckBuyItem_buyVanish:
.setupBuyItem modelID_VanishCap, behavior_VanishCap, cost_VanishCap

func_ShopCheckBuyItem_buyStarShell:
.setupBuyItem modelID_Star, behavior_Shell, cost_StarShell

func_ShopCheckBuyItem_buy1UP:
.setupBuyItem modelID_1UP, behavior_1UP, cost_1UP

func_ShopCheckBuyItem_buyItem:
.buyItem

func_ShopCheckBuyItem_end:
lw s0, 0x10 (sp) ; Restore s0 to what value it was before
lw ra, 0x14 (sp)
jr ra
addiu sp, sp, 0x18
.endarea
