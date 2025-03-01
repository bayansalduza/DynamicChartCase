# Dynamic Chart Application – Introduction

This application is designed to dynamically retrieve data from various database objects (tables, views, stored procedures, functions) and visualize that data using a selected chart type (bar, line, radar, etc.). The solution is built with an **ASP.NET Core Web API** (C#) on the server side, which provides a robust service architecture, and a modern, interactive front-end developed using **HTML, CSS, and JavaScript** (jQuery, Bootstrap, Chart.js).

> **Note:** The project utilizes the **Dapper** library to enhance performance and flexibility in database operations. Dapper is a lightweight, high-performance micro-ORM based on ADO.NET, which enables efficient execution of SQL queries directly.

---

## 1. Overall Objective

- **Dynamic Charts:**  
  The user can select a database object (table, view, or stored procedure) and view the returned data as a chart rendered with Chart.js.

- **Listing Data Sources:**  
  The application displays a list of database objects (Table, View, StoredProcedure, Function) available for selection.

- **Column Mapping:**  
  The returned data columns can be mapped by the user to “Label” and “Value” fields, converting the data into a format suitable for Chart.js.

- **Multiple Chart Types:**  
  Various chart types (Bar, Line, Radar, etc.) are supported. The user can choose a chart type and dynamically generate a corresponding chart.

---

## 2. Technologies Used

- **ASP.NET Core Web API (C#):**
  - Multi-layered architecture (Domain, Application, Infrastructure, Web API).
  - Fast and flexible SQL queries are executed using **Dapper**.
  - The API is documented using Swagger.

- **Database (SQL Server):**
  - Example database: `ChartSampleDB`
  - Example tables: Data sets such as `SalesData` and `ExpensesData`.
  - Support for stored procedures and views is included.

- **Front-End:**
  - Built with **HTML, CSS, and JavaScript** (jQuery, Bootstrap, Chart.js).
  - Provides an interactive interface for dynamic data retrieval, column mapping, and chart rendering.

---

## 3. Application Workflow (Step by Step)

1. **Database Connection Input:**  
   The user enters the server name, database name, username, and password, then clicks the *"Test Connection"* button. This request is processed by the `ConnectionController` in the API.

2. **Listing Data Sources:**  
   Once the connection is successful, the user clicks the *"Load Data Sources"* button. The API’s `MetadataController` retrieves and returns a list of database objects (tables, views, stored procedures, functions).

3. **Chart Type Selection and Data Retrieval:**  
   The user selects the desired data source from the list and chooses a chart type (bar, line, radar). Upon clicking the *"Get Data"* button, the `ChartDataController` in the API executes the appropriate query—using `SELECT * FROM [ObjectName]` for tables and views or `EXEC [ObjectName]` for stored procedures—and returns the data.

4. **Mapping and Chart Rendering:**  
   The retrieved data is stored on the front-end. The user maps the columns to “Label” and “Value” fields. Clicking the *"Draw Chart"* button uses Chart.js to render the chart on a canvas. The *"Clear Chart"* button removes the current chart, allowing for new selections and re-rendering.

---

## 4. Connection and User Information (Example)

- **Server Name:** `92.205.130.157\SQLEXPRESS,1433`
- **Database:** `ChartSampleDB`
- **Login:** `ChartSampleUser`
- **Password:** `ChartSamplePassword123.`

These credentials are used for connecting to the API.

---

## 5. Project Links

- **API Swagger URL:** [https://dynamic-chart-api.bakibayansalduz.com/swagger/index.html](https://dynamic-chart-api.bakibayansalduz.com/swagger/index.html)
- **Front-End URL:** [https://dynamic-chart.bakibayansalduz.com/](https://dynamic-chart.bakibayansalduz.com/)
- **GitHub FE Repo URL:** [https://github.com/bayansalduza/DynamicChartCaseFE](https://github.com/bayansalduza/DynamicChartCaseFE)

---

## 6. Example Data Structures

- **Table:**  
  `SalesData` – For instance, containing columns such as `SalesDate`, `Category`, and `Amount` for sales data.

- **View:**  
  Views that present summarized data (e.g., `v_SalesSummary`).

- **Stored Procedure:**  
  Procedures like `sp_GetMonthlySales` or `sp_GetMonthlyExpenses` that return aggregated values by month and category.

- **Function:**  
  Table-valued functions (parameterized or non-parameterized).

---

## 7. Extensibility Options

- **Parameter Input:**  
  Users can enter additional parameters for stored procedures or functions.

- **Advanced Chart Configuration:**  
  Options such as colors, legends, tooltips, and responsive settings can be customized.

- **Authentication:**  
  For multi-user scenarios, JWT or session-based authentication can be added.

- **Additional Chart Types:**  
  More chart types such as Pie, Doughnut, or PolarArea can be integrated.

---

## 8. Conclusion

The **Dynamic Chart Application** transforms data from various database objects into dynamic charts using **HTML, jQuery, and Chart.js**. The **ASP.NET Core Web API** layer manages database connectivity, retrieves lists of tables/views/SPs, and returns the data as JSON to the front-end. On the client side, users can select a chart type and map data columns to create interactive, dynamic charts.

This solution follows the workflow:  
**"Connect to Database → Select Object → Choose Chart Type → Map Columns → Render Chart"**  
providing a dynamic and extensible chart visualization tool.
