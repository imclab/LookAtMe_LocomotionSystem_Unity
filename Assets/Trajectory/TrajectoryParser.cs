using System.Collections;
using System.Collections.Generic;
using System.IO;

/***
 * This class is used to parse classic trajectory files (.csv)
 * The .csv file is formatted as follows: time;x;y
 * The decimal separator is the dot character ('.')
 ***/

public class TrajectoryParser {

	protected string file;
	protected char comma;

	public TrajectoryParser(string _file) {
		if (_file == null || _file == "") {
			throw new System.ArgumentException("File name is empty.");
		}

		file = _file;
		comma = ';';
	}

	public string File {
		set { file = value; }
	}

	public TrajectoryParser(string _file, char _comma) {
		if (_file == null || _file == "") {
			throw new System.ArgumentException("File name is empty.");
		}
		
		file = _file;
		comma = _comma;
	}

	public virtual List<TrajectoryPoint> parse() {
		string[] lines;
		lines = System.IO.File.ReadAllLines(file);

		return readPoints(lines);
	}
	
	protected List<TrajectoryPoint> readPoints(string[] lines, int j) {
		List<TrajectoryPoint> trajectory = new List<TrajectoryPoint>();

		// Sometimes, we have "time;x;y" as the first line.
		// j: number of lines to jump before starting reading.
		for (int i = j; i < lines.Length; ++i) {
			string[] split = lines[i].Split(comma);
			trajectory.Add(new TrajectoryPoint(float.Parse(split[1]),
											   float.Parse(split[2]),
											   float.Parse(split[0])));
		}

		return trajectory;
	}

	protected List<TrajectoryPoint> readPoints(string[] lines) {
		return readPoints(lines, 1);
	}
}
