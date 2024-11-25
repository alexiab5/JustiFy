# JustiFy

JustiFy is a C# program that formats and justifies text paragraphs. It ensures clean and professional text alignment, distributing spaces evenly to create visually appealing paragraphs. Perfect for formatting large documents or displaying text with precision.

# Formatting Rules
1. Paragraphs: Each paragraph is formatted independently, preserving word order.
2. Line Construction: Lines contain as many words as possible without exceeding the maximum width.
3. Space Distribution:
  * Empty spaces are distributed uniformly across word gaps.
  * Extra spaces are added to the left-most gaps when uniform distribution isn’t possible.
4. Last Line: The last line of a paragraph is left-aligned, with single spaces between words.

# Usage
### Command-Line Arguments
The program requires three arguments:
1. Input File Name: Name of the file containing the text to be justified.
2. Output File Name: Name of the file to save the formatted text.
3. Maximum Text Width: Maximum number of characters allowed per line.
