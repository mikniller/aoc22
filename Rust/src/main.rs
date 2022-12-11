use std::fs;
use std::io::{ self, BufRead };
use std::collections::{ HashMap, HashSet };

fn main() {
    day1();
    day2();
    day3();
}

fn score3(c: char) -> i32 {
    let cval: i32 = c as i32;
    let aval: i32 = 'a' as i32;
    let aval_upper: i32 = 'A' as i32;
    if cval < aval {
        return cval - aval_upper + 27;
    } else {
        return cval - aval + 1;
    }
}

fn day3() {
    let mut sum: i32 = 0;
    let mut sum1: i32 = 0;
    for l in read_file_lines(3, false) {
        let (f, s) = l.split_at(l.len() / 2);
        let f1: HashSet<char> = f.chars().collect::<Vec<char>>().into_iter().collect();
        let l1: HashSet<char> = s.chars().collect::<Vec<char>>().into_iter().collect();
        let mut ins = f1.intersection(&l1);
        let elemscore = score3(*ins.next().unwrap());
        sum = sum + elemscore;
    }

    let lines = read_file_lines(3, false);
    let mut index: usize = 0;
    while index < lines.len() {
        let f1: HashSet<char> = lines[index].chars().collect::<Vec<char>>().into_iter().collect();
        let f2: HashSet<char> = lines[index + 1].chars().collect::<Vec<char>>().into_iter().collect();
        let f3: HashSet<char> = lines[index + 2].chars().collect::<Vec<char>>().into_iter().collect();
        let f1_2:HashSet<char> = f1.intersection(&f2).collect::<Vec<&char>>().into_iter().map(|s| *s).collect();
        let mut ins=f1_2.intersection(&f3);
        let elemscore = score3(*ins.next().unwrap());
        sum1 = sum1 + elemscore;
        index = index + 3;
    }

    println!("Day3: {} {}", sum, sum1);
}


fn day2() {
    let lookup = HashMap::from([
        ('X', 1),
        ('Y', 2),
        ('Z', 3),
    ]);
    let score = HashMap::from([
        ("A X", 3),
        ("A Y", 6),
        ("A Z", 0),
        ("B X", 0),
        ("B Y", 3),
        ("B Z", 6),
        ("C X", 6),
        ("C Y", 0),
        ("C Z", 3),
    ]);
    let translate = HashMap::from([
        ("A X", "A Z"),
        ("A Y", "A X"),
        ("A Z", "A Y"),
        ("B X", "B X"),
        ("B Y", "B Y"),
        ("B Z", "B Z"),
        ("C X", "C Y"),
        ("C Y", "C Z"),
        ("C Z", "C X"),
    ]);

    let mut sum: i32 = 0;
    let mut sum1: i32 = 0;

    for l in read_file_lines(2, false) {
        let mut c: char = l.chars().nth(2).unwrap();

        sum += lookup.get(&c).unwrap();
        sum += score.get(&*l).unwrap();

        let converted = translate.get(&*l).unwrap();
        c = converted.chars().nth(2).unwrap();

        sum1 += lookup.get(&c).unwrap();
        sum1 += score.get(&*converted).unwrap();
    }
    println!("Day2: {} {}", sum, sum1);
}

fn day1() {
    let mut sum: i32 = 0;
    let mut sums: Vec<i32> = Vec::new();

    for l in read_file_lines(1, false) {
        if l.is_empty() {
            sums.push(sum);
            sum = 0;
        } else {
            sum = sum + l.parse::<i32>().unwrap();
        }

    }
    sums.sort_by(|a, b| b.cmp(a));
    let p1: i32 = sums.iter().take(1).sum();
    let p2: i32 = sums.iter().take(3).sum();

    println!("Day1: {} {}", p1, p2);
}

fn read_file_lines(day: u8, is_sample: bool) -> Vec<String> {
    let mut str = String::new();
    let f = if is_sample { "_sample" } else { "" };
    let file_name = format!("D:\\dev\\aoc\\data\\day{}{}.txt", day, f);
   // println!("{}", file_name);

    let contents = fs::read_to_string(file_name).expect("Error reading file");
    let lines = contents
        .lines()
        .map(|s| s.to_string())
        .collect::<Vec<String>>();
    return lines;
}