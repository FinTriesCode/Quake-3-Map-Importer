<img width="939" alt="Screenshot 2025-04-15 at 14 32 49" src="https://github.com/user-attachments/assets/c2be2255-a4e5-4b96-abeb-93cb7ecba171" />How to use  


1. Extract contents of folder into Unity.

2. Create empty game object.

3. Attach `BSPImporter` script to newly added game object.  

4. Ensure path and file name inside of the `BSPImporter` script match that of your desired .BSP file. 
  4.1. This has to be done in script as makming this string publically accessibly forces the streamed data to be static - something I want to avoid.

5. Run project.

