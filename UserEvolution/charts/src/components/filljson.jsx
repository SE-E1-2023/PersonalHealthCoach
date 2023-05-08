export default function filljson(personalDataList) {
  const fileName = "weight.json";

  // Filter data for the last 7 days
  const lastSevenDaysData = personalDataList.filter((item) => {
    const date = new Date(item.CreatedAt);
    const now = new Date();
    const daysDifference = Math.round((now - date) / (1000 * 60 * 60 * 24));
    return daysDifference <= 7;
  });

  // Transform data into the desired format
  const newData = lastSevenDaysData.map((item) => {
    return { label: item.CreatedAt, value: item.weight };
  });

  // Write the data to the JSON file
  const updatedJson = JSON.stringify(newData, null, 2);
  const blob = new Blob([updatedJson], { type: "application/json" });
  const url = URL.createObjectURL(blob);
  const link = document.createElement("a");
  link.download = fileName;
  link.href = url;
  link.click();
}
