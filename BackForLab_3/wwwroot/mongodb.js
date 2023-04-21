db.Musics.aggregate(
  {
    $match: {
      _id:ObjectId("643ed64300000c000c000036")
    }
  },
  {
    $lookup: {
      from: "Grades",
      localField:"_id",
      foreignField:"Music",
      as:"Grades"
    }
  },
  {
    $project: {
      "Grade": {
        $avg: "$Grades.Grade"
      },
      "Grade":1
    }
  }
);